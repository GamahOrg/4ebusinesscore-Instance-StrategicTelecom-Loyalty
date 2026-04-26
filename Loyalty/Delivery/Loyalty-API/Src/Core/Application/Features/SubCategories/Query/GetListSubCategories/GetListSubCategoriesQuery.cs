namespace _4EPlatform.Application.Features.SubCategories.Query.GetListSubCategories
{
    public class GetListSubCategoriesQuery : IRequest<ResponseDTO>
    {
        public Guid CategoryId { get; set; }
        public Status? Status { get; set; }
    }
}
