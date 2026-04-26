namespace _4EPlatform.Application.Features.ServiceProviderFeature.Query.GetAllServiceProvider
{
    public class GetAllServiceProviderCommandHandler : IRequestHandler<GetAllServiceProviderCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _repository;
        private readonly IResponseHelper _responseHelper;
        public ResponseDTO _responseDTO;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;


        public GetAllServiceProviderCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork repository, IResponseHelper response, IResponseHelper responseHelper)
        {

            _stringLocalizer = stringLocalizer;
            _repository = repository;
            _responseHelper = responseHelper;
            _responseDTO = new ResponseDTO();
        }
        public async Task<ResponseDTO> Handle(GetAllServiceProviderCommand request, CancellationToken cancellationToken)
        {

            var filterConditions = new Dictionary<Expression<Func<ServiceProvider, bool>>, bool>();

            if (!string.IsNullOrEmpty(request.Search))
            {
                filterConditions.Add(x => x.NameAr == request.Search ||
                                          x.NameAr.Contains(request.Search) || x.NameEn == request.Search || x.NameEn.Contains(request.Search)
                                          , true);
            }
            var listServiceProvider = _repository.Repository<ServiceProvider>().GetAllAsNoTracking(x => x.State != State.Deleted)

                    .ApplyFilters(filterConditions, request.PageNumber, request.PageSize, ref _responseDTO);

            if (listServiceProvider == null)
                _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);




            if (request.LatestOffers == true)
            {
                listServiceProvider = _repository.Repository<ServiceProvider>().GetAllAsNoTracking(x => x.State != State.Deleted)

                    .OrderByDescending(x => x.CreatedOn)

                    .ApplyFilters(filterConditions, request.PageNumber, request.PageSize, ref _responseDTO);
            }
            if (request.MostFavorite == true)
            {
                listServiceProvider = _repository.Repository<ServiceProvider>().GetAllAsNoTracking(x => x.State != State.Deleted && x.ServiceProviderLikes.Count() > 0)

                   .OrderByDescending(x => x.ServiceProviderLikes.Count()).ApplyFilters(filterConditions, request.PageNumber, request.PageSize, ref _responseDTO);


            }
            if (request.MostUsed == true)
            {
                listServiceProvider = listServiceProvider
                  ?.OrderByDescending(x => x.Offers
                  .Count(o => o.OfferLikeUseds.Any(ol => ol.OfferType == OfferType.Used)))
                  .ApplyFilters(filterConditions, request.PageNumber, request.PageSize, ref _responseDTO);
            }
            if (request.MostDiscountedOffers == true)
            {
                listServiceProvider = listServiceProvider
                   ?.OrderByDescending(x => x.Offers.Count(o => o.DiscountAmount > 0))
                   .ApplyFilters(filterConditions, request.PageNumber, request.PageSize, ref _responseDTO);
            }
            var Result = listServiceProvider?.Select(x => new GetAllServiceProviderDto
            {
                Id = x.Id,
                Image = x.Image,
                NameAr = x.NameAr,
                NameEn = x.NameEn,
                TitelAr = x.TitleAr,
                TitleEn = x.TitleEn,
                offerCount = x.Offers.Count(),
            }).ToList();
            if (listServiceProvider == null)
                _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);
            _responseDTO.Result = Result;
            _responseDTO.StatusEnum = StatusEnum.Success;
            _responseDTO.Message = _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully];
            _responseDTO.TotalPages = _responseDTO.TotalPages;
            _responseDTO.TotalItems = _responseDTO.TotalItems;
            _responseDTO.PageSize = _responseDTO.PageSize;
            _responseDTO.PageIndex = _responseDTO.PageIndex;
            return _responseDTO;
        }
    }
}
