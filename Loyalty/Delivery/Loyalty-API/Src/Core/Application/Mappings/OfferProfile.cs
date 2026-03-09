
using _4EPlatform.Application.Features.Offers.Command;

namespace _4EPlatform.Application.Mappings
{
    public partial class MappingProfile
    {
        public void OfferMapping()
        {
            CreateMap<OfferDTO, Offer>()

            .ForMember(dest => dest.ImagePth, opt => opt.Ignore())
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.StartTime)))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.EndTime)));

            CreateMap<PostOfferDTO, Offer>().
            ForMember(dest => dest.ImagePth, opt => opt.Ignore())
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.StartTime)))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.EndTime)));

            CreateMap<PutOfferDTO, Offer>().
       ForMember(dest => dest.ImagePth, opt => opt.Ignore())
       .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.StartTime)))
       .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.EndTime)));



            CreateMap<ServiceProviderOfferDTO, Offer>()

          .ForMember(dest => dest.ImagePth, opt => opt.Ignore())
          .ForMember(dest => dest.ServiceProviderId, opt => opt.Ignore())
          .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.StartTime)))
          .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.EndTime)));

            CreateMap<Offer, Features.Offers.Query.DTO.GetOfferDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.NameAr))
                .ForMember(dest => dest.ServiceProviderNameAr, opt => opt.MapFrom(src => src.ServiceProvider.NameAr))
                .ForMember(dest => dest.ServiceProviderNameEn, opt => opt.MapFrom(src => src.ServiceProvider.NameEn))
                .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.SubCategory.NameAr))
                .ForMember(dest => dest.DiscountTypeName, opt => opt.MapFrom(src => src.Lk_DiscountType.NameAr))
                .ForMember(dest => dest.DiscountMethodName, opt => opt.MapFrom(src => src.Lk_DiscountMethod.NameAr))
                .ForMember(dest => dest.serviceProviderLocation, opt => opt.MapFrom(src => src.ServiceProvider.Location))
                .ForMember(dest => dest.ServiceProviderImage, opt => opt.MapFrom(src => src.ServiceProvider.Image))
                .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategory.Id))
                .ForMember(dest => dest.ServiceProviderId, opt => opt.MapFrom(src => src.ServiceProvider.Id))
                .ForMember(dest => dest.serviceProviderwebSite, opt => opt.MapFrom(src => src.ServiceProvider.Website));

        }
    }
}
