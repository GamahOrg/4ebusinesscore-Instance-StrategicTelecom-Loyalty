namespace _4EPlatform.Application.Features.Lk_DiscountMethodFeature.Command.Delete
{
    public class DeleteDiscountMethodCommand : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
        public DeleteDiscountMethodCommand(Guid id)
        {
            Id = id;
        }
    }
}

