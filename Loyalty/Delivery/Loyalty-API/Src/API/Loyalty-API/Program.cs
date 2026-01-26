
using _4EPlatform.Application.Resources;
using _4EPlatform.PermissionsGroupingChecker;
using _4EPlatform.PremessionRegister;
using _4EPlatform_Controlpanel_WorkSpace.GrpClientsConfig;
using Microsoft.Extensions.Localization;
using UploadHelper.Helpers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = int.MaxValue; // for IIS
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 104857600; // 100 MB
});
builder.Services.AddUnitOfWorkRepository();
builder.Services.AddDiServices();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 7256, o => o.Protocols = HttpProtocols.Http2);

    // ADDED THIS LINE to fix the problem
    options.Listen(IPAddress.Any, 5138, o => o.Protocols = HttpProtocols.Http1);

});

builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging(true);
});
builder.Services.AddLocalizationOptions();

builder.Services.AddUserManagementGrpcClients(builder.Configuration["GrpcServiceSettings:UserManagmentUrl"]);
builder.Services.AddHostedService<AddPermissionsMiddleware>();
builder.Services.AddScoped<PermissionsGroupingCheckerMiddleware, PermissionsGroupingCheckerMiddleware>();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://elasticsearch:9200"))
    {
        AutoRegisterTemplate = true,
        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
        ModifyConnectionSettings = conn => conn.DisableDirectStreaming()
    })
    .CreateLogger();


builder.Host.UseSerilog();

// Add services to the container.
var logger = new LoggerConfiguration()
          .ReadFrom.Configuration(builder.Configuration)
          .Enrich.FromLogContext()
          .CreateLogger();
builder.Host.UseSerilog(logger);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "StrategicTelecom-EmployeeLoyalty APIs Reference",
    });

    c.AddSecurityDefinition(
    "token",
    new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Name = HeaderNames.Authorization
    }
        );
    c.AddSecurityRequirement(
    new OpenApiSecurityRequirement
    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "token"
                            },
                        },
                        Array.Empty<string>()
                    }
    }
        );
});
#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddRateLimiting(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCors",
        builder =>
        {
            builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:3000")
            .SetIsOriginAllowed((host) => true)
            .AllowCredentials();
        });
});


builder.Services.AddAuthentication("Bearer")
      .AddJwtBearer("Bearer", options =>
      {
          // the name of your api resources   
          options.Audience = "UserManagementServer";
          /// identity server url                    
          options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
          options.RequireHttpsMetadata = false;
      });
builder.Services.AddVersioning();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ResourceOwnerPolicy", policy =>
    {
        policy.AddAuthenticationSchemes("Bearer");
        policy.RequireClaim("scope", "4EMicroServices"); // Require scope associated with Resource Owner Password token
    });

    options.AddPolicy("ClientCredentialsPolicy", policy =>
    {
        policy.AddAuthenticationSchemes("Bearer");
        policy.RequireClaim("scope", "CPAggregatorAPI"); // Require scope associated with Client Credentials token
    });
});
builder.Services.AddGrpc();
var app = builder.Build();
var localizer = app.Services.GetRequiredService<IStringLocalizer<SharedResource>>();
Upload.Initialize(localizer);
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDBContext>();
    var serviceProvider = serviceScope.ServiceProvider;
    if (!serviceScope.ServiceProvider.GetService<AppDBContext>().AllMigrationsApplied())
    {
        serviceScope.ServiceProvider.GetService<AppDBContext>().Migrate();
    }
    ContextSeed.Seed(dbContext, serviceProvider,builder.Configuration);
}

app.UseHttpsRedirection();


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
    RequestPath = "/wwwroot"
});
#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
#endregion
app.MapControllers();

app.UseCors("AllowCors");
app.UseStaticFiles();
//app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandler>();
app.MapControllers();
app.UseSerilogRequestLogging();
app.AddMapGRPCServicesApp();
app.UseRateLimiter();
app.UseMiddleware<PermissionsGroupingCheckerMiddleware>();
app.Run();
