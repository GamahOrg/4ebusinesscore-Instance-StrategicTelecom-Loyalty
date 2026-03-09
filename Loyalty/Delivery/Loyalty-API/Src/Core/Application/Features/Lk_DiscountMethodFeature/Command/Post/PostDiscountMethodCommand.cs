

namespace _4EPlatform.Application.Features.Lk_DiscountMethodFeature.Command.Post
{
    public class PostDiscountMethodCommand : IRequest<ResponseDTO>
    {
        public DiscountMethodDTO DTO { get; set; }
        public PostDiscountMethodCommand(DiscountMethodDTO dto)
        {
            DTO = dto;
        }
    }
}
