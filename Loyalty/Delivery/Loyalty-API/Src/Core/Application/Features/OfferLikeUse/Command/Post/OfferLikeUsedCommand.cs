namespace _4EPlatform.Application.Features.OfferLikeUse.Command.Post
{
    public class OfferLikeUsedCommand : IRequest<ResponseDTO>
    {
        public Guid OfferId { get; set; }
        public OfferTypeDto OfferType { get; set; }
    }
    public enum OfferTypeDto
    {
        Used = 1,
        Like = 2,
        Unlike
    }
}
