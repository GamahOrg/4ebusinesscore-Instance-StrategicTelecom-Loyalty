namespace _4EPlatform.Application.Features.ServiceProviderFeature.Query.GetAll
{
    public class GetAllServiceProvidersHandler : IRequestHandler<GetAllServiceProviderQuery, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        public ResponseDTO _ResponseDTO;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public GetAllServiceProvidersHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
            _ResponseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO> Handle(GetAllServiceProviderQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAllServiceProviderValidation(_unitOfWork, _stringLocalizer);
            var validation = validator.Validate(request);
            if (!validation.IsValid)
                return _responseHelper.FailedToSave(string.Join("-", validation.Errors.Select(n => n.ErrorMessage).ToList()));

            var filterConditions = new Dictionary<Expression<Func<ServiceProvider, bool>>, bool>();

            if (!string.IsNullOrEmpty(request.SearchWord))
            {
                filterConditions.Add(x => x.NameAr == request.SearchWord ||
                                          x.NameAr.Contains(request.SearchWord) || x.NameEn == request.SearchWord || x.NameEn.Contains(request.SearchWord)
                                          , true);
            }
            if (!string.IsNullOrEmpty(request.CategoryId.ToString()))
            {

                filterConditions.Add(x => x.Offers.Any(s => s.CategoryId == request.CategoryId), true);
            }
            if (!string.IsNullOrEmpty(request.SubCategoryId.ToString()))
            {
                filterConditions.Add(x => x.Offers.Any(s => s.SubCategoryId == request.SubCategoryId), true);
            }




            var ServiceProviders = await _unitOfWork.Repository<ServiceProvider>().GetAllAsNoTracking(x => x.State == State.NotDeleted)
                .OrderByDescending(x => x.CreatedOn)

                .ApplyFilters(filterConditions, request.PageNumber, request.PageSize, ref _ResponseDTO)
                .Select(x => new GetServiceProviderDTO
                {

                    Id = x.Id,
                    NameAr = x.NameAr,
                    NameEn = x.NameEn,
                    TitelAr = x.TitleAr,
                    TitleEn = x.TitleEn,
                    Email = x.Email,
                    Mobile = x.Mobile,
                    ServiceProviderImage = x.Image,
                    Website = x.Website,
                    OfferCounts = x.Offers.Count(),
                    Status = x.Status,
                    CreateOn = x.CreatedOn,
                    Location = x.Location,
                }).ToListAsync(cancellationToken);

            if (ServiceProviders.Count() <= 0)
            {
                return _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.NotFound]);
            }

            return _responseHelper.RetrievedSuccessfully(ServiceProviders, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully], ref _ResponseDTO);
        }

    }

}

