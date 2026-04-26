using Microsoft.Extensions.Configuration;

namespace _4EPlatform.Application.Features.Offers.Query.GetAllWithFilter
{
    public class GetAllOfferWithFilterCommandHandler : IRequestHandler<GetAllOfferWithFilterCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _repository;
        private readonly IResponseHelper _responseHelper;
        public ResponseDTO _responseDTO;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IConfiguration _configuration;


        public GetAllOfferWithFilterCommandHandler(IConfiguration configuration, IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork repository, IResponseHelper response, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;
            _responseHelper = responseHelper;
            _configuration = configuration;
            _responseDTO = new ResponseDTO();
        }
        public async Task<ResponseDTO> Handle(GetAllOfferWithFilterCommand request, CancellationToken cancellationToken)
        {
            int timeZoneOffset = _configuration.GetValue<int>("GlobalSettings:TimeDifference");
            DateTime nationalDateTime = DateTime.Now.AddHours(timeZoneOffset);
            var currentDate = DateOnly.FromDateTime(nationalDateTime);
            var currentTime = TimeOnly.FromDateTime(nationalDateTime);


            var filterConditions = new Dictionary<Expression<Func<Offer, bool>>, bool>();

            if (!string.IsNullOrEmpty(request.Search))
            {
                filterConditions.Add(x => x.NameAr == request.Search ||
                                          x.NameAr.Contains(request.Search) || x.NameEN == request.Search || x.NameEN.Contains(request.Search)
                                          , true);
            }
            if (!string.IsNullOrEmpty(request.CategoryId.ToString()))
            {
                filterConditions.Add(x => x.CategoryId == request.CategoryId, true);
            }
            if (!string.IsNullOrEmpty(request.SubCategoryId.ToString()))
            {
                filterConditions.Add(x => x.SubCategoryId == request.SubCategoryId, true);
            }

            var Offer = _repository.Repository<Offer>().GetAllAsNoTracking(x => x.State != State.Deleted)

                .AsEnumerable() // force the next part to run in memory
                .Where(x => ((x.StartDate.Add(x.StartTime)) <= nationalDateTime && (x.EndDate.Add(x.EndTime)) >= nationalDateTime))
             .AsQueryable().ApplyFilters(filterConditions, request.PageNumber, request.PageSize, ref _responseDTO);


            if (request.LatestOffers == true)
            {
                Offer = _repository.Repository<Offer>().GetAllAsNoTracking(x => x.State != State.Deleted)
                  .AsEnumerable() // force the next part to run in memory
                .Where(x => ((x.StartDate.Add(x.StartTime)) <= nationalDateTime && (x.EndDate.Add(x.EndTime)) >= nationalDateTime))
             .AsQueryable().OrderByDescending(x => x.CreatedOn)
                .ApplyFilters(filterConditions, request.PageNumber, request.PageSize, ref _responseDTO);
            }
            if (request.MostFavorite == true)
            {

                Offer = _repository.Repository<Offer>().GetAllAsNoTracking(x => x.State != State.Deleted)

                     .AsEnumerable() // force the next part to run in memory
                .Where(x => ((x.StartDate.Add(x.StartTime)) <= nationalDateTime && (x.EndDate.Add(x.EndTime)) >= nationalDateTime))
             .AsQueryable()
                 ?.OrderByDescending(x => x.OfferLikeUseds.Where(x => x.OfferType == OfferType.Like).Count())
                 .ApplyFilters(filterConditions, request.PageNumber, request.PageSize, ref _responseDTO);
            }
            if (request.MostUsed == true)
            {
                Offer = Offer
                  ?.OrderByDescending(x => x.OfferLikeUseds.Where(x => x.OfferType == OfferType.Used).Count());
            }
            if (request.MostDiscountedOffers == true)
            {
                Offer = Offer
                    ?.OrderByDescending(o => o.DiscountAmount);
            }
            var Result = Offer?.Select(x => new GetAllOfferWithFilterDto
            {
                Id = x.Id,
                ImagePth = x.ImagePth,
                NameAr = x.NameAr,
                NameEN = x.NameEN,
                DiscountAmount = x.DiscountAmount,
                DescriptionAEn = x.DescriptionAEn,
                DescriptionAr = x.DescriptionAr,
                ServiceProviderNameAr = x.ServiceProvider.NameAr,
                ServiceProviderNameEn = x.ServiceProvider.NameEn,
                ServiceProviderImage = x.ServiceProvider.Image,
                EndDate = x.EndDate,

            }).ToList();
            if (Offer.Count() <= 0)
                _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound] ?? "Error Localizer");
            _responseDTO.Result = Result;
            _responseDTO.StatusEnum = StatusEnum.Success;
            _responseDTO.Message = _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "Error Localizer";
            _responseDTO.TotalPages = _responseDTO.TotalPages;
            _responseDTO.TotalItems = _responseDTO.TotalItems;
            _responseDTO.PageSize = _responseDTO.PageSize;
            _responseDTO.PageIndex = _responseDTO.PageIndex;
            return _responseDTO;
        }
    }
}
