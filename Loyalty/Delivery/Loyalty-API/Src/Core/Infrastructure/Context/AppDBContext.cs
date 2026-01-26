


namespace _4EPlatForm.Infrastructure.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public virtual DbSet<ServiceProviderLike> ServiceProviderLikes { get; set; }
        public virtual DbSet<OfferLikeUsed> OfferLikeUseds { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Lk_DiscountMethod> Lk_DiscountMethods { get; set; }
        public virtual DbSet<LK_OfferType> LK_OfferType { get; set; }
        public virtual DbSet<Lk_DiscountType> Lk_DiscountTypes { get; set; }
        public virtual DbSet<ServiceProviderImage> ServiceProviderImages { get; set; }
        public virtual DbSet<ServiceProvider> ServiceProviders { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        public bool AllMigrationsApplied()
        {
            var applied = this.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = this.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        public void Migrate()
        {
            this.Database.Migrate();
        }
    }
}
