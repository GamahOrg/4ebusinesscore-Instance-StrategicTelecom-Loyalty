using _4EPlatform.Application.Features.Offers.Query.DTO;

namespace _4EPlatform.Application.Features.Offers.Query.CountOfferInDiscountMethod
{
    public class CountOfferInDiscountMethodHandler : IRequestHandler<CountOfferInDiscountMethodQuery, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        public ResponseDTO _ResponseDTO;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public CountOfferInDiscountMethodHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
            _ResponseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO> Handle(CountOfferInDiscountMethodQuery request, CancellationToken cancellationToken)
        {


            var NumberDiscountMethodOffers = await _unitOfWork.Repository<Lk_DiscountMethod>().GetAllAsNoTracking(x => x.State == State.NotDeleted)


                .Select(item => new LookupIndicatorDTO
                {
                    LookupId = item.Id,
                    LookupName = item.NameAr + "-" + item.NameEn,
                    offerCount = item.Offers.Count()

                })

                .ToListAsync();



            if (NumberDiscountMethodOffers.Count <= 0)
            {
                return _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.NotFound] ?? "Error Localizer");
            }
            _ResponseDTO.Result = NumberDiscountMethodOffers;
            return _responseHelper.RetrievedSuccessfully(NumberDiscountMethodOffers, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "Error Localizer");
        }

    }

}

