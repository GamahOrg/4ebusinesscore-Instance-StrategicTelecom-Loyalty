

namespace _4EPlatform.Application.Features.Lk_DiscountMethodFeature.Query.GetAll
{
    public class GetAllDiscountMethodQueryHandler : IRequestHandler<GetAllDiscountMethodQuery, ResponseDTO>
    {
        #region Fields
        private readonly IUnitOfWork _repository;
        private IResponseHelper _responseDTO;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public GetAllDiscountMethodQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork repository, IMapper mapper, IResponseHelper response)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;
            _responseDTO = response;
            _mapper = mapper;
        }
        #endregion
        #region Handler
        public async Task<ResponseDTO> Handle(GetAllDiscountMethodQuery request, CancellationToken cancellationToken)
        {
            var listDiscountMethod = await _repository.Repository<Lk_DiscountMethod>().GetAllNoTracking().
                Select(item => new GetDiscountMethodDTO
                {
                    Id = item.Id,
                    NameAr = item.NameAr,
                    NameEn = item.NameEn,
                    IsSeed = item.IsSeed,
                }).ToListAsync(cancellationToken);

            if (listDiscountMethod == null)
                return _responseDTO.NotFound(_stringLocalizer[SharedResourceKeys.NotFound] ?? "Error Localizations");


            return _responseDTO.RetrievedSuccessfully(listDiscountMethod, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "Error Localizations");
        }
        #endregion
    }
}

