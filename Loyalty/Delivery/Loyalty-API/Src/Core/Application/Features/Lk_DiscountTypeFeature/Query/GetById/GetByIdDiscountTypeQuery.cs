namespace _4EPlatform.Application.Features.Lk_DiscountTypeFeature.Query.GetById
{
    public class GetByIdDiscountTypeQuery : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
        public GetByIdDiscountTypeQuery(Guid id)
        {
            Id = id;
        }
    }
}
