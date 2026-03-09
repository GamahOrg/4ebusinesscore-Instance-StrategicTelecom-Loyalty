namespace _4EPlatform.Application.Features.Offers.Query.GetById
{
    public class GetOfferByIdQuery : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
        public GetOfferByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
