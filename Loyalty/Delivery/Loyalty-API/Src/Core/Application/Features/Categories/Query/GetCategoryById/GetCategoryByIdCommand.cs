namespace _4EPlatform.Application.Features.Categories.Query.GetCategoryById
{
    public class GetCategoryByIdCommand : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
    }
}
