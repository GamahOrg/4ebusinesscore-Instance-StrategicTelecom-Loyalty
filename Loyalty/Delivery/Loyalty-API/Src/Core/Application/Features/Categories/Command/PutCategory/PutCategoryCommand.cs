namespace _4EPlatform.Application.Features.Categories.Command.PutCategory
{
    public class PutCategoryCommand : IRequest<ResponseDTO>
    {
        public PutCategoryPram PutCategoryPram { get; set; }
    }
}
