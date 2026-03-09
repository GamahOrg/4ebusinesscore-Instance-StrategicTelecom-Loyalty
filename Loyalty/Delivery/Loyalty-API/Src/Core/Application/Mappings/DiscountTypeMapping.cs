using _4EPlatform.Application.Features.Lk_DiscountTypeFeature.Command;

namespace _4EPlatform.Application.Mappings
{
    public partial class MappingProfile
    {
        public void DiscountTypeMapping()
        {
            CreateMap<DiscountTypeDTO, Lk_DiscountType>();
            CreateMap<Lk_DiscountType, GetDiscountTypeDTO>();
        }
    }
}
