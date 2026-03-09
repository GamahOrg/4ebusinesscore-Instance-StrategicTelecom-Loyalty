namespace _4EPlatform.Application.Features.Lk_DiscountTypeFeature.Command.Delete
{
    public class DeleteDiscountTypeCommand : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
        public DeleteDiscountTypeCommand(Guid id)
        {
            Id = id;
        }
    }
}

