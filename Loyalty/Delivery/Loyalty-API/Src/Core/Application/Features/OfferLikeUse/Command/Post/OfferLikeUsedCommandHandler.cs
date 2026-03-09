

namespace _4EPlatform.Application.Features.OfferLikeUse.Command.Post
{
    public class OfferLikeUsedCommandHandler : IRequestHandler<OfferLikeUsedCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        public Guid _loggedInUserId;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public OfferLikeUsedCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper, IHttpContextAccessor httpContextAccessor)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
            _loggedInUserId = LoggedInUserProvider.GetLoggedInUserId(httpContextAccessor);
        }
        public async Task<ResponseDTO> Handle(OfferLikeUsedCommand request, CancellationToken cancellationToken)
        {
            var offerold = _unitOfWork.Repository<Offer>()
                 .GetAllAsNoTracking(x => x.Id == request.OfferId).FirstOrDefault();
            if (offerold == null)
            {
                return _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.NotFound]);

            }
            if (offerold.EndDate < DateTime.UtcNow)
            {
                return _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.OfferIsExpired]);
            }

            var offer = _unitOfWork.Repository<OfferLikeUsed>()
                    .GetAllAsNoTracking(x => x.OfferId == request.OfferId
                                        && x.UserId == _loggedInUserId
                                        //&& request.OfferType == OfferTypeDto.Unlike
                                        && x.OfferType == OfferType.Like
                                        // || x.OfferType == (OfferType)request.OfferType
                                        )
                                        .FirstOrDefault();
            if (offer != null)
            {

                if (request.OfferType == OfferTypeDto.Unlike && offer.OfferType == OfferType.Like)
                {
                    offer.State = State.Deleted;
                    await _unitOfWork.Repository<OfferLikeUsed>().UpdateAsync(offer);
                }
                else if (request.OfferType != (OfferTypeDto)offer.OfferType && request.OfferType != OfferTypeDto.Unlike)
                {
                    var newOffer = new OfferLikeUsed
                    {
                        OfferId = request.OfferId,
                        UserId = _loggedInUserId,
                        OfferType = (OfferType)request.OfferType,
                    };

                    await _unitOfWork.Repository<OfferLikeUsed>().AddAsync(newOffer);
                }

            }
            else if (request.OfferType != OfferTypeDto.Unlike)
            {
                var newOffer = new OfferLikeUsed
                {
                    OfferId = request.OfferId,
                    UserId = _loggedInUserId,
                    OfferType = (OfferType)request.OfferType,
                };

                await _unitOfWork.Repository<OfferLikeUsed>().AddAsync(newOffer);
            }

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.AddSuccessfully]);

        }
    }
}
