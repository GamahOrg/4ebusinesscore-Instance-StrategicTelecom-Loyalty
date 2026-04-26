using _4EPlatform.Application.Features.Command;
using _4EPlatform.Application.Features.ServiceProviderFeature.Command.Post;
using GetOfferDTO = _4EPlatform.Application.Features.Offers.Query.GetAll.GetOfferDTO;

namespace _4EPlatform.Application.Mappings
{
    public partial class MappingProfile
    {
        public void ServiceProviderMapping()
        {
            CreateMap<PutServiceProviderDTO, ServiceProvider>()

            .ForMember(dest => dest.Image, opt => opt.Ignore())
            .ForMember(dest => dest.ServiceProviderImages, opt => opt.Ignore());

            CreateMap<PostServiceProviderDTO, ServiceProvider>()

           .ForMember(dest => dest.Image, opt => opt.Ignore())
           .ForMember(dest => dest.ServiceProviderImages, opt => opt.Ignore());



            CreateMap<ServiceProviderWithOfferDTO, ServiceProvider>()

            .ForMember(dest => dest.Image, opt => opt.Ignore())
            .ForMember(dest => dest.ServiceProviderImages, opt => opt.Ignore())


            .ForMember(dest => dest.Offers, opt => opt.Ignore());



            CreateMap<ServiceProvider, GetServiceProviderDTO>();
            CreateMap<ServiceProvider, GetServiceProviderListDTO>()
                  .ForMember(dest => dest.ServiceProviderImage, opt => opt.MapFrom(src => src.ServiceProviderImages))
                  .ForMember(dest => dest.Offers, opt => opt.MapFrom(src => src.Offers));
            CreateMap<ServiceProviderImage, GetServiceProviderImageDTO>();
            CreateMap<Offer, GetOfferDTO>();

        }
    }
}
