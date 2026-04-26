namespace _4EPlatform.Application.Features.Lk_OfferType.Query.GetById
{
    public class GetByIdLK_OfferTypeQuery : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
        public GetByIdLK_OfferTypeQuery(Guid id)
        {
            Id = id;
        }

    }
}
