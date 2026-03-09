namespace _4EPlatform.Application.Features.ServiceProviderLiked.Query
{
    public class CountServiceProviderLikesHandler : IRequestHandler<CountServiceProviderLikesQuery, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        public ResponseDTO _ResponseDTO;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public CountServiceProviderLikesHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
            _ResponseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO> Handle(CountServiceProviderLikesQuery request, CancellationToken cancellationToken)
        {


            var CountServiceProviderLikes = await _unitOfWork.Repository<ServiceProviderLike>().GetAllAsNoTracking(x => x.State == State.NotDeleted).CountAsync();

            if (CountServiceProviderLikes <= 0)
            {
                return _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.NotFound] ?? "Error Localizer");
            }
            _ResponseDTO.Result = CountServiceProviderLikes;
            return _responseHelper.RetrievedSuccessfully(CountServiceProviderLikes, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "Error Localizer");
        }

    }
}
