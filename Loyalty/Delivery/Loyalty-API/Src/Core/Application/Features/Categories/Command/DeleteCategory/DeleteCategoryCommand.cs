namespace _4EPlatform.Application.Features.Categories.Command.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
    }
}
