using _4EPlatform.Application.Features.Lk_OfferType.Query.DTO;

namespace _4EPlatform.Application.Features.Lk_OfferType.Query.GetById
{
    public class GetByIdlk_offerTypeQueryHandler : IRequestHandler<GetByIdLK_OfferTypeQuery, ResponseDTO>
    {
        #region Fields
        private readonly IUnitOfWork _repository;
        private IResponseHelper _responseDTO;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public GetByIdlk_offerTypeQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork repository, IMapper mapper, IResponseHelper response)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;
            _responseDTO = response;
            _mapper = mapper;
        }
        #endregion
        #region Handler
        public async Task<ResponseDTO> Handle(GetByIdLK_OfferTypeQuery request, CancellationToken cancellationToken)
        {
            var LKOfferType = await _repository.Repository<LK_OfferType>().GetAllAsNoTracking(e => e.Id == request.Id).FirstOrDefaultAsync();

            if (LKOfferType == null)
                return _responseDTO.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);

            var LKOfferTypeMapper = _mapper.Map<GetLK_OfferTypeDTO>(LKOfferType);

            return _responseDTO.RetrievedSuccessfully(LKOfferTypeMapper, _stringLocalizer[SharedResourceKeys.AddSuccessfully]);
        }
        #endregion

    }
}
