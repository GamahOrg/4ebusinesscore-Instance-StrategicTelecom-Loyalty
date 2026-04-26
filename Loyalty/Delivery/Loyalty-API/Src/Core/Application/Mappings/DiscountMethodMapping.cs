namespace _4EPlatform.Application.Mappings
{
    public partial class MappingProfile
    {
        public void DiscountMethodMapping()
        {
            CreateMap<DiscountMethodDTO, Lk_DiscountMethod>();
            CreateMap<Lk_DiscountMethod, GetDiscountMethodDTO>();
        }
    }
}
