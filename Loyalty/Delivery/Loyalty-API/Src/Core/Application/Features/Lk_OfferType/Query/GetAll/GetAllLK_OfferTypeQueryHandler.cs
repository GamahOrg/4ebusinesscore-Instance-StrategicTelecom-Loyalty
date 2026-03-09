using _4EPlatform.Application.Features.Lk_OfferType.Query.DTO;

namespace _4EPlatform.Application.Features.Lk_OfferType.Query.GetAll
{
    public class GetAllLK_OfferTypeQueryHandler : IRequestHandler<GetAllLK_OfferTypeQuery, ResponseDTO>
    {
        #region Fields
        private readonly IUnitOfWork _repository;
        private IResponseHelper _responseDTO;

        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public GetAllLK_OfferTypeQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork repository, IResponseHelper response)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;
            _responseDTO = response;

        }
        #endregion
        #region Handler
        public async Task<ResponseDTO> Handle(GetAllLK_OfferTypeQuery request, CancellationToken cancellationToken)
        {
            var listDiscountMethod = await _repository.Repository<LK_OfferType>().GetAllNoTracking().
                Select(item => new GetLK_OfferTypeDTO
                {
                    Id = item.Id,
                    NameAr = item.NameAr,
                    NameEn = item.NameEn,
                }).ToListAsync();

            if (listDiscountMethod == null)
                return _responseDTO.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);


            return _responseDTO.RetrievedSuccessfully(listDiscountMethod, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully]);
        }
        #endregion
    }

}

