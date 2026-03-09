namespace _4EPlatform.Application.Features.Command.Post
{
    public class PostLK_OfferTypeCommand : IRequest<ResponseDTO>
    {
        public DiscountTypeDTO DTO { get; set; }
        public PostLK_OfferTypeCommand(DiscountTypeDTO dto)
        {
            DTO = dto;
        }

    }
}
