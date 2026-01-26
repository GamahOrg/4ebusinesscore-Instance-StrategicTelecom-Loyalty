using MicroBlocks.Application;

namespace _4EPlatform_StrategicTelecom_EmployeeLoyalty.API.Extension
{
    public static class ServiceExtension
    {

        public static void AddMapGRPCServicesApp(this WebApplication app)
        {

        }

        public static void AddUnitOfWorkRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IGRepository<>), typeof(GRepository<>));


        }
        public static void AddDiServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TokenService).Assembly);
            //services.AddAutoMapper(AppDomain.CurrentDomain.Load("4EPlatform.Application"));
            services.AddAutoMapper((typeof(ApplicationLayer)));
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR((typeof(ApplicationLayer)));
            services.AddScoped<GlobalExceptionHandler, GlobalExceptionHandler>();
            services.AddScoped(typeof(IResponseHelper), typeof(ResponseHelper));
            services.AddSingleton<ITokenService, TokenService>();
        }

        public static void AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRateLimiter(options =>
            {
                var rateLimitSection = configuration.GetSection("RateLimit");
                var permitLimit = rateLimitSection.GetValue<int>("PermitLimit");
                var queueLimit = rateLimitSection.GetValue<int>("QueueLimit");
                var windowSeconds = rateLimitSection.GetValue<int>("WindowSeconds");
                options.AddFixedWindowLimiter("FixedWindowPolicy", opt =>
                {
                    opt.Window = TimeSpan.FromSeconds(windowSeconds);
                    opt.PermitLimit = permitLimit;
                    opt.QueueLimit = queueLimit;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                }).RejectionStatusCode = 429;
            });
        }

        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning().AddApiExplorer(options =>
            {

                options.GroupNameFormat = "'v'VVV";


            });
        }
        public static void AddLocalizationOptions(this IServiceCollection services)
        {
            services.AddLocalization(options => { options.ResourcesPath = ""; });




            services.Configure<RequestLocalizationOptions>(options =>
            {
                List<CultureInfo> supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("de-DE"),
                    new CultureInfo("fr-FR"),
                    new CultureInfo("ar")
                };
                options.DefaultRequestCulture = new RequestCulture("ar");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }
    }
}
