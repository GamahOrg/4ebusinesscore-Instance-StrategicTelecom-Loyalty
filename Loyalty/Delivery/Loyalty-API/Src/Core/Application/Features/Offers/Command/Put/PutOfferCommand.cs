namespace _4EPlatform.Application.Features.Offers.Command.Put
{
    public class PutOfferCommand : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }

        public PutOfferDTO DTO { get; set; }

    }
}
