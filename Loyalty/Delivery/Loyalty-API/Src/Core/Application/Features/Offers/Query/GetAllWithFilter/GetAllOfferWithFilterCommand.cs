namespace _4EPlatform.Application.Features.Offers.Query.GetAllWithFilter
{
    public class GetAllOfferWithFilterCommand : IRequest<ResponseDTO>
    {
        public string? Search { get; set; }
        public bool? LatestOffers { get; set; }
        public bool? MostFavorite { get; set; }
        public bool? MostUsed { get; set; }
        public bool? MostDiscountedOffers { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
    }
}
