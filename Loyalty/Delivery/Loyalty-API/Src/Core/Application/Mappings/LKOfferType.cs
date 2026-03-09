using _4EPlatform.Application.Features.Command.DTO;

namespace _4EPlatform.Application.Mappings
{


    public partial class MappingProfile
    {
        public void Lk_OfferLikeMapping()
        {
            CreateMap<LK_OfferTypeDTO, LK_OfferType>();
            CreateMap<LK_OfferType, LK_OfferTypeDTO>();
        }
    }
}

