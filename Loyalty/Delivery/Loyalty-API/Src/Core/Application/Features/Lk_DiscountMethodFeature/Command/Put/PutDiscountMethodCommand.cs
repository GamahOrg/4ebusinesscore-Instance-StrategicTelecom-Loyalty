namespace _4EPlatform.Application.Features.Lk_DiscountMethodFeature.Command.Put
{
    public class PutDiscountMethodCommand : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
        public DiscountMethodDTO DTO { get; set; }
        public PutDiscountMethodCommand(Guid id, DiscountMethodDTO dto)
        {
            Id = id;
            DTO = dto;
        }

    }
}
