namespace _4EPlatform.Application.Features.Offers.Query.GetOffersActivelyMonthly
{
    internal class OffersActivelyMonthlyHandler : IRequestHandler<OffersActivelyMonthlyQuery, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        public ResponseDTO _ResponseDTO;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public OffersActivelyMonthlyHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
            _ResponseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO> Handle(OffersActivelyMonthlyQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var startOfNextMonth = startOfMonth.AddMonths(1);

            var offersActivelyMonthly = await _unitOfWork.Repository<Offer>()
                .GetAllAsNoTracking(x =>
                    x.State == State.NotDeleted &&
                    x.EndDate >= now && // still valid
                    x.StartDate < startOfNextMonth && // started before next month
                    x.EndDate >= startOfMonth // ends in or after current month
                )
                .CountAsync();

            if (offersActivelyMonthly <= 0)
            {
                return _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.NotFound] ?? "Error Localizer");
            }
            _ResponseDTO.Result = offersActivelyMonthly;

            return _responseHelper.RetrievedSuccessfully(offersActivelyMonthly, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "Error Localizer", ref _ResponseDTO);
        }

    }

}

