

using GetOfferDTO = _4EPlatform.Application.Features.Offers.Query.DTO.GetOfferDTO;

namespace _4EPlatform.Application.Features.Offers.Query.GetById
{
    public class GetOfferByIdQueryHandler : IRequestHandler<GetOfferByIdQuery, ResponseDTO>
    {
        #region Fields
        private readonly IUnitOfWork _repository;
        private IResponseHelper _responseDTO;
        private readonly IMapper _mapper;
        public Guid _loggedInUserId;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public GetOfferByIdQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork repository, IMapper mapper, IResponseHelper response, IHttpContextAccessor _httpContextAccessor)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;
            _responseDTO = response;
            _mapper = mapper;
            _loggedInUserId = LoggedInUserProvider.GetLoggedInUserId(_httpContextAccessor);
        }
        #endregion
        #region Handler
        public async Task<ResponseDTO> Handle(GetOfferByIdQuery request, CancellationToken cancellationToken)
        {
            var offer = await _repository.Repository<Offer>().GetAllAsNoTracking(e => e.Id == request.Id).FirstOrDefaultAsync();

            if (offer == null)
                return _responseDTO.NotFound(_stringLocalizer[SharedResourceKeys.NotFound] ?? "Error Localizer");

            var offerMapper = _mapper.Map<GetOfferDTO>(offer);


            offerMapper.Liked = _repository.Repository<OfferLikeUsed>().GetAllAsNoTracking(e => e.OfferType == OfferType.Like && e.UserId == _loggedInUserId && e.OfferId == offer.Id).Any() ? true : false;

            return _responseDTO.RetrievedSuccessfully(offerMapper, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "Error Localizer");
        }
        #endregion
    }
}

