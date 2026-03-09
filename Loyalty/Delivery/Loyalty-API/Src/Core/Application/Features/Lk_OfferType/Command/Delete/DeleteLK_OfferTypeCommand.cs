namespace _4EPlatform.Application.Features.Lk_OfferType.Command.Delete
{
    public class DeleteLK_OfferTypeCommand : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
        public DeleteLK_OfferTypeCommand(Guid id)
        {
            Id = id;
        }

    }
}
