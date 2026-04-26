namespace _4EPlatform.Application.Features.Offers.Command.Delete
{
    public class DeleteOfferCommand : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }

    }

}
