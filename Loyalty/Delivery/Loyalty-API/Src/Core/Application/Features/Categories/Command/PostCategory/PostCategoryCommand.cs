namespace _4EPlatform.Application.Features.Categories.Command.PostCategory
{
    public class PostCategoryCommand : IRequest<ResponseDTO>
    {
        public PostCategoryPram PostCategoryPram { get; set; }
    }
}
