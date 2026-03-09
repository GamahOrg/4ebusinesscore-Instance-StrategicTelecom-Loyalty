namespace _4EPlatform.Application.Features.Offers.Query.AverageDiscountAmount
{
    public class CountAverageDiscountAmountHandler : IRequestHandler<CountAverageDiscountAmountQuery, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        public ResponseDTO _ResponseDTO;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public CountAverageDiscountAmountHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
            _ResponseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO> Handle(CountAverageDiscountAmountQuery request, CancellationToken cancellationToken)
        {


            var Offers = await _unitOfWork.Repository<Offer>().GetAllAsNoTracking(x => x.State == State.NotDeleted).CountAsync();


            var SumAmount = await _unitOfWork.Repository<Offer>().GetAllAsNoTracking(x => x.State == State.NotDeleted).SumAsync(x => x.DiscountAmount);

            if (Offers <= 0)
            {
                return _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.NotFound] ?? "Error Localizer");
            }
            var amount = SumAmount / Offers;
            _ResponseDTO.Result = amount;
            return _responseHelper.RetrievedSuccessfully(amount, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "Error Localizer");
        }

    }
}
