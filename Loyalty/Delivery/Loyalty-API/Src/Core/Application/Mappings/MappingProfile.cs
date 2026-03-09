namespace _4EPlatform.Application.Mappings
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CategoryMapping();
            OfferMapping();
            ServiceProviderMapping();
            DiscountMethodMapping();
            DiscountTypeMapping();
            SubCategoryMapping();
            Lk_OfferLikeMapping();
        }


    }
}
