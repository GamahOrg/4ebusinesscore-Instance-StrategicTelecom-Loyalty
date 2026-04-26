namespace _4EPlatform.Application.Features.Lk_DiscountTypeFeature.Command.Put
{
    public class PutDiscountTypeCommand : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
        public DiscountTypeDTO DTO { get; set; }
        public PutDiscountTypeCommand(Guid id, DiscountTypeDTO dto)
        {
            Id = id;
            DTO = dto;
        }
    }
}
