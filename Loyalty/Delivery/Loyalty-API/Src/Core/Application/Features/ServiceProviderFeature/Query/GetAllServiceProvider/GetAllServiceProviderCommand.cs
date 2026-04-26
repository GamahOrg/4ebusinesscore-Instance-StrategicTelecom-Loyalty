namespace _4EPlatform.Application.Features.ServiceProviderFeature.Query.GetAllServiceProvider
{
    public class GetAllServiceProviderCommand : IRequest<ResponseDTO>
    {
        public string? Search { get; set; }
        public bool? LatestOffers { get; set; }
        public bool? MostFavorite { get; set; }
        public bool? MostUsed { get; set; }
        public bool? MostDiscountedOffers { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
