namespace _4EPlatform.Application.Features.Offers.Query.GetOffersAvailable
{


    public class OffersAvailableHandler : IRequestHandler<OffersAvailableQuery, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        public ResponseDTO _ResponseDTO;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public OffersAvailableHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
            _ResponseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO> Handle(OffersAvailableQuery request, CancellationToken cancellationToken)
        {


            var NumberAvailableOffers = await _unitOfWork.Repository<Offer>().GetAllAsNoTracking(x => x.State == State.NotDeleted && x.EndDate >= DateTime.Now).CountAsync();

            if (NumberAvailableOffers <= 0)
            {
                return _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.NotFound] ?? "Error Localizer");
            }
            _ResponseDTO.Result = NumberAvailableOffers;
            return _responseHelper.RetrievedSuccessfully(NumberAvailableOffers, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "Error Localizer");
        }

    }


}


