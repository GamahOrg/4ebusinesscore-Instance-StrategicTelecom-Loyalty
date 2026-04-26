namespace _4EPlatform.Application.Features.Categories.Query.GetListCategory
{
    public class GetListCategoryQuery : IRequest<ResponseDTO>
    {
        public Status? Status { get; set; }
    }
}
