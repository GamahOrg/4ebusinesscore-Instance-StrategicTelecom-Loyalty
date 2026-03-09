using _4EPlatform.Application.Features.Offers.Query.DTO;
using _4EPlatform.Application.Features.Offers.Query.OffersInDiscountAmount;

namespace _4EPlatform.Application.Features.Offers.Query.OffersInDiscountType
{
    public class CountOfferInDiscountTypeHandler : IRequestHandler<CountOfferInDiscountTypeQuery, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        public ResponseDTO _ResponseDTO;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public CountOfferInDiscountTypeHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
            _ResponseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO> Handle(CountOfferInDiscountTypeQuery request, CancellationToken cancellationToken)
        {


            var NumberDiscountTypeOffers = await _unitOfWork.Repository<Lk_DiscountType>().GetAllAsNoTracking(x => x.State == State.NotDeleted)


                .Select(item => new LookupIndicatorDTO
                {
                    LookupId = item.Id,
                    LookupName = item.NameAr + "-" + item.NameEn,
                    offerCount = item.Offers.Count()

                })

                .ToListAsync();



            if (NumberDiscountTypeOffers.Count() <= 0)
            {
                return _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.NotFound] ?? "Error Localizer");
            }
            _ResponseDTO.Result = NumberDiscountTypeOffers;
            return _responseHelper.RetrievedSuccessfully(NumberDiscountTypeOffers, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "Error Localizer");
        }

    }

}

