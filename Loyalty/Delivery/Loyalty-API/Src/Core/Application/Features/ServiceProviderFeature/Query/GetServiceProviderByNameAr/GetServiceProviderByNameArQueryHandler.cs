namespace _4EPlatform.Application.Features.ServiceProviderFeature.Query.GetServiceProviderByNameAr
{
    public class GetServiceProviderByNameArQueryHandler : IRequestHandler<GetServiceProviderByNameArQuery, ResponseDTO>
    {
        #region Fields
        private readonly IUnitOfWork _repository;
        private ResponseDTO _responseDTO;


        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        #endregion
        #region Constructor
        public GetServiceProviderByNameArQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork repository, IResponseHelper response)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;
            _responseDTO = new ResponseDTO();


        }
        #endregion
        #region Handler
        public async Task<ResponseDTO> Handle(GetServiceProviderByNameArQuery request, CancellationToken cancellationToken)
        {
            var serviceProvider = await _repository.Repository<ServiceProvider>().GetAllAsNoTracking(e => e.NameAr == request.NameAr).FirstOrDefaultAsync();

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

