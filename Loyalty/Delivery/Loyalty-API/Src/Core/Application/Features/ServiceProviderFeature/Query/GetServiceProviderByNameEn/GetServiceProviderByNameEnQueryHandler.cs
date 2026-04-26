namespace _4EPlatform.Application.Features.ServiceProviderFeature.Query.GetServiceProviderByNameEn
{
    public class GetServiceProviderByNameEnQueryHandler : IRequestHandler<GetServiceProviderByNameEnQuery, ResponseDTO>
    {
        #region Fields
        private readonly IUnitOfWork _repository;
        private ResponseDTO _responseDTO;


        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        #endregion
        #region Constructor
        public GetServiceProviderByNameEnQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork repository, IResponseHelper response)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;
            _responseDTO = new ResponseDTO();


        }
        #endregion
        #region Handler
        public async Task<ResponseDTO> Handle(GetServiceProviderByNameEnQuery request, CancellationToken cancellationToken)
        {
            var serviceProvider = await _repository.Repository<ServiceProvider>().GetAllAsNoTracking(e => e.NameEn == request.NameEn).FirstOrDefaultAsync();

            if (serviceProvider == null)

            {
                _responseDTO.Message = _stringLocalizer[SharedResourceKeys.NotFound];

                _responseDTO.StatusEnum = StatusEnum.FailedToFindTheObject;
                _responseDTO.Result = false;
                return _responseDTO;
            }


            _responseDTO.Message = _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully];
            _responseDTO.StatusEnum = StatusEnum.Success;
            _responseDTO.Result = true;
            return _responseDTO;



        }
        #endregion
    }


}

