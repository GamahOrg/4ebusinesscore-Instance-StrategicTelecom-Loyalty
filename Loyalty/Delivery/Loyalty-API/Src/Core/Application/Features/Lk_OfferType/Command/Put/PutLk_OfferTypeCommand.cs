

namespace _4EPlatform.Application.Features.Lk_OfferType.Command.Put
{
    public class PutLk_OfferTypeCommand : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
        public LK_OfferTypeDTO DTO { get; set; }
        public PutLk_OfferTypeCommand(Guid id, LK_OfferTypeDTO dto)
        {
            Id = id;
            DTO = dto;
        }

    }
}
