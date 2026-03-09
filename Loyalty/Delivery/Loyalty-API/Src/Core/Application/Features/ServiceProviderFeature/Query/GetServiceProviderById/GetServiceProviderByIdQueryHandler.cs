namespace _4EPlatform.Application.Features.ServiceProviderFeature.Query.GetServiceProviderById
{
    public class GetServiceProviderByIdQueryHandler : IRequestHandler<GetServiceProviderByIdQuery, ResponseDTO>
    {
        #region Fields
        private readonly IUnitOfWork _repository;
        private IResponseHelper _responseDTO;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public Guid _loggedInUserId;
        #endregion
        #region Constructor
        public GetServiceProviderByIdQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork repository, IMapper mapper, IResponseHelper response, IHttpContextAccessor _httpContextAccessor)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;
            _responseDTO = response;
            _mapper = mapper;
            _loggedInUserId = LoggedInUserProvider.GetLoggedInUserId(_httpContextAccessor);
        }
        #endregion
        #region Handler
        public async Task<ResponseDTO> Handle(GetServiceProviderByIdQuery request, CancellationToken cancellationToken)
        {
            var serviceProvider = await _repository.Repository<ServiceProvider>().GetAllAsNoTracking(e => e.Id == request.Id).FirstOrDefaultAsync();

            if (serviceProvider == null)
                return _responseDTO.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);

            var serviceProviderMapper = _mapper.Map<GetServiceProviderListDTO>(serviceProvider);
            serviceProviderMapper.Liked = _repository.Repository<ServiceProviderLike>().GetAllAsNoTracking(e => e.State != State.Deleted && e.UserId == _loggedInUserId && e.ServiceProviderId == serviceProvider.Id).Any() ? true : false;

            return _responseDTO.RetrievedSuccessfully(serviceProviderMapper, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully]);
        }
        #endregion
    }
}
