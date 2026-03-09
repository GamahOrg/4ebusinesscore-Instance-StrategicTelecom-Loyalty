

namespace _4EPlatform.Application.Features.Lk_DiscountTypeFeature.Query.GetAll
{
    public class GetAllDiscountTypeQueryHandler : IRequestHandler<GetAllDiscountTypeQuery, ResponseDTO>
    {
        #region Fields
        private readonly IUnitOfWork _repository;
        private IResponseHelper _responseDTO;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public GetAllDiscountTypeQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork repository, IMapper mapper, IResponseHelper response)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;
            _responseDTO = response;
            _mapper = mapper;
        }
        #endregion
        #region Handler
        public async Task<ResponseDTO> Handle(GetAllDiscountTypeQuery request, CancellationToken cancellationToken)
        {
            var listDiscountType = await _repository.Repository<Lk_DiscountType>().GetAllNoTracking()
                .Select(item => new GetDiscountTypeDTO
                {
                    Id = item.Id,
                    NameAr = item.NameAr,
                    NameEn = item.NameEn,
                    IsSeed = item.IsSeed,
                }).ToListAsync(cancellationToken);

            if (listDiscountType == null)
                return _responseDTO.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);
            return _responseDTO.RetrievedSuccessfully(listDiscountType, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully]);
        }
        #endregion
    }
}

