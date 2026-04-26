namespace _4EPlatform.Application.Features.Lk_DiscountTypeFeature.Command.Post
{
    public class PostDiscountTypeCommand : IRequest<ResponseDTO>
    {
        public DiscountTypeDTO DTO { get; set; }
        public PostDiscountTypeCommand(DiscountTypeDTO dto)
        {
            DTO = dto;
        }
    }
}
