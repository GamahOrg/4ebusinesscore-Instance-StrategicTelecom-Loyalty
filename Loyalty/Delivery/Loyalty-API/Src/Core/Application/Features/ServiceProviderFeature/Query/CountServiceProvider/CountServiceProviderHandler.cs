namespace _4EPlatform.Application.Features.ServiceProviderFeature.Query.CountServiceProvider
{
    public class CountServiceProviderHandler : IRequestHandler<CountServiceProviderQuery, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        public ResponseDTO _ResponseDTO;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public CountServiceProviderHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
            _ResponseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO> Handle(CountServiceProviderQuery request, CancellationToken cancellationToken)
        {


            var CountServiceProvider = await _unitOfWork.Repository<ServiceProvider>().GetAllAsNoTracking(x => x.State == State.NotDeleted).CountAsync();

            if (CountServiceProvider <= 0)
            {
                return _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.NotFound] ?? "Error Localizer");
            }
            _ResponseDTO.Result = CountServiceProvider;
            return _responseHelper.RetrievedSuccessfully(CountServiceProvider, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "Error Localizer");
        }
    }
}

