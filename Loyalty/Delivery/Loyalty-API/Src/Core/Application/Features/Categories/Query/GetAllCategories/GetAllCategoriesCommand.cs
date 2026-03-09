namespace _4EPlatform.Application.Features.Categories.Query.GetAllCategories
{
    public class GetAllCategoriesCommand : IRequest<ResponseDTO>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
        public Status? Status { get; set; }
    }
}
