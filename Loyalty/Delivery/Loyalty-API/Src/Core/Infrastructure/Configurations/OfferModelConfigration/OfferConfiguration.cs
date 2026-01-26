namespace _4EPlatform.Infrastructure.Configurations.OfferModelConfiguration
{
    public class OfferConfiguration : GenericConfiguration<Offer>
    {
        public override void Configure(EntityTypeBuilder<Offer> builder)
        {

            builder.HasOne(o => o.SubCategory)
                .WithMany(s => s.Offers)
                .HasForeignKey(o => o.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascading issues

            base.Configure(builder);
        }
    }
}
