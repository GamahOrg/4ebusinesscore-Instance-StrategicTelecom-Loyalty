namespace _4EPlatform.Application.Features.Lk_DiscountMethodFeature.Query.GetById
{
    public class GetByIdDiscountMethodQuery : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
        public GetByIdDiscountMethodQuery(Guid id)
        {
            Id = id;
        }
    }
}
