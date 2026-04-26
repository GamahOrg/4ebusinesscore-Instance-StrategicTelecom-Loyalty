namespace _4EPlatform.Application.Features.Offers.Query.GetByServiceProviderId
{
    public class GetOffersByIServiceProviderQuery : IRequest<ResponseDTO>
    {
        public Guid ServiceProviderId { get; set; }
        public GetOffersByIServiceProviderQuery(Guid id)
        {
            ServiceProviderId = id;
        }

    }
}
