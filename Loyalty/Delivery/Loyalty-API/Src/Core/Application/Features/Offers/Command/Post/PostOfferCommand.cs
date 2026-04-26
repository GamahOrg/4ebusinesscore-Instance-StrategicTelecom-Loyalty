namespace _4EPlatform.Application.Features.Offers.Command.Post
{
    public class PostOfferCommand : IRequest<ResponseDTO>
    {
        public PostOfferDTO DTO { get; set; }
    }
}
