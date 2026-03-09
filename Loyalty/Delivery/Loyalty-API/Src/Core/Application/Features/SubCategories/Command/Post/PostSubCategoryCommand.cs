namespace _4EPlatform.Application.Features.SubCategories.Command.Post
{
    public class PostSubCategoryCommand : IRequest<ResponseDTO>
    {
        public PostSubCategoryDTO DTO { get; set; }
    }
}
