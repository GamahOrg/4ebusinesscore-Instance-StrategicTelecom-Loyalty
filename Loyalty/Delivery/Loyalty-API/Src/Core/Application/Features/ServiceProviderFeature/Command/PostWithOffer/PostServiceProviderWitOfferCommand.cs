namespace _4EPlatform.Application.Features.ServiceProviderFeature.Command.PostWithOffer
{
    public class PostServiceProviderWitOfferCommand : IRequest<ResponseDTO>
    {

        public ServiceProviderWithOfferDTO DTO { get; set; }
    }
}
