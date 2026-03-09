

namespace _4EPlatform.Application.Features.ServiceProviderFeature.Query.GetListServiceProvider
{
    public class GetListServiceProviderQueryHandler : IRequestHandler<GetListServiceProviderQuery, ResponseDTO>
    {
        #region Fields
        private readonly IUnitOfWork _repository;
        private IResponseHelper _responseDTO;

        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public GetListServiceProviderQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork repository, IResponseHelper response)
        {
            _stringLocalizer = stringLocalizer;

            _repository = repository;
            _responseDTO = response;

        }
        #endregion
        #region Handler
        public async Task<ResponseDTO> Handle(GetListServiceProviderQuery request, CancellationToken cancellationToken)
        {
            var listServiceProvider = await _repository.Repository<ServiceProvider>().GetAllAsNoTracking(x => (request.Status == null || x.Status == request.Status))
                .OrderByDescending(x => x.CreatedOn).Select(x => new GetServiceProviderDTO
                {
                    Id = x.Id,
                    NameAr = x.NameAr,
                    NameEn = x.NameEn,
                    Image = x.Image
                })
                .ToListAsync();

            if (!listServiceProvider.Any())
                return _responseDTO.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);



            return _responseDTO.RetrievedSuccessfully(listServiceProvider, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully]);
        }
        #endregion
    }
}
