namespace _4EPlatform.Application.Features.Lk_DiscountTypeFeature.Query.GetById
{
    public class GetByIdDiscountTypeQueryHandler : IRequestHandler<GetByIdDiscountTypeQuery, ResponseDTO>
    {
        #region Fields
        private readonly IUnitOfWork _repository;
        private IResponseHelper _responseDTO;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public GetByIdDiscountTypeQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork repository, IMapper mapper, IResponseHelper response)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;
            _responseDTO = response;
            _mapper = mapper;
        }
        #endregion
        #region Handler
        public async Task<ResponseDTO> Handle(GetByIdDiscountTypeQuery request, CancellationToken cancellationToken)
        {
            var DiscountType = await _repository.Repository<Lk_DiscountType>().GetAllAsNoTracking(e => e.Id == request.Id).FirstOrDefaultAsync();

            if (DiscountType == null)
                return _responseDTO.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);

            var listDiscountTypeMapper = _mapper.Map<GetDiscountTypeDTO>(DiscountType);

            return _responseDTO.RetrievedSuccessfully(listDiscountTypeMapper, _stringLocalizer[SharedResourceKeys.AddSuccessfully]);
        }
        #endregion
    }
}

