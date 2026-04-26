namespace _4EPlatform.Application.Features.Offers.Query.GetAll
{
    public class GetAllOfferQuery : IRequest<ResponseDTO>
    {

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string? SearchWord { get; set; }
        public DateTime? StartDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? EndTime { get; set; }

        public Status status { get; set; }
        public Guid CategoryId { get; set; }

        public Guid SubCategoryId { get; set; }

    }
}
