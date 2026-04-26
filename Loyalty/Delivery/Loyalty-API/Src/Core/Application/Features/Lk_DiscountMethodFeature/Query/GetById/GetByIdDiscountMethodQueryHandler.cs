namespace _4EPlatform.Application.Features.Lk_DiscountMethodFeature.Query.GetById
{
    public class GetByIdDiscountMethodQueryHandler : IRequestHandler<GetByIdDiscountMethodQuery, ResponseDTO>
    {
        #region Fields
        private readonly IUnitOfWork _repository;
        private IResponseHelper _responseDTO;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public GetByIdDiscountMethodQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork repository, IMapper mapper, IResponseHelper response)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;
            _responseDTO = response;
            _mapper = mapper;
        }
        #endregion
        #region Handler
        public async Task<ResponseDTO> Handle(GetByIdDiscountMethodQuery request, CancellationToken cancellationToken)
        {
            var DiscountMethod = await _repository.Repository<Lk_DiscountMethod>().GetAllAsNoTracking(e => e.Id == request.Id).FirstOrDefaultAsync();

            if (DiscountMethod == null)
                return _responseDTO.NotFound(_stringLocalizer[SharedResourceKeys.NotFound] ?? "Error Localizations");

            var listDiscountMethodMapper = _mapper.Map<GetDiscountMethodDTO>(DiscountMethod);

            return _responseDTO.RetrievedSuccessfully(listDiscountMethodMapper, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "Error Localizations");
        }
        #endregion
    }
}

