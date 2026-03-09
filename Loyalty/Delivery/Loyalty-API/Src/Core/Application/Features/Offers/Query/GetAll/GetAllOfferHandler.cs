using Microsoft.Extensions.Configuration;

namespace _4EPlatform.Application.Features.Offers.Query.GetAll
{
    public class GetAllOfferHandler : IRequestHandler<GetAllOfferQuery, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        public ResponseDTO _ResponseDTO;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IConfiguration _configuration;

        public GetAllOfferHandler(IConfiguration configuration, IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
            _ResponseDTO = new ResponseDTO();
            _configuration = configuration;
        }

        public async Task<ResponseDTO> Handle(GetAllOfferQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAllFilterOfferValidation(_stringLocalizer);
            var validation = validator.Validate(request);

            if (!validation.IsValid)
                return _responseHelper.FailedToSave(JsonSerializer.Serialize(validation.Errors.Select(n => n.ErrorMessage).ToList()));

            var filterConditions = new Dictionary<Expression<Func<Offer, bool>>, bool>();

            if (!string.IsNullOrEmpty(request.SearchWord))
            {
                filterConditions.Add(x => x.NameAr == request.SearchWord ||
                                          x.NameAr.Contains(request.SearchWord) || x.NameEN == request.SearchWord || x.NameEN.Contains(request.SearchWord)
                                          , true);
            }





            if (request.StartDate != null)
            {

                filterConditions.Add(x => x.StartDate == request.StartDate, true);
            }

            if (request.StartTime != null)
            {

                filterConditions.Add(x => x.StartTime == request.StartTime, true);
            }
            if (request.EndDate != null)
            {

                filterConditions.Add(x => x.EndDate == request.EndDate, true);
            }

            if (request.status != 0)
            {

                filterConditions.Add(x => x.Status == request.status, true);
            }

            if (request.EndTime != null)
            {

                filterConditions.Add(x => x.EndTime == request.EndTime, true);
            }
            if (request.CategoryId != Guid.Empty)
            {
                filterConditions.Add(x => x.CategoryId == request.CategoryId, true);
            }
            if (request.SubCategoryId != Guid.Empty)
            {
                filterConditions.Add(x => x.SubCategoryId == request.SubCategoryId, true);
            }
            var offers = await _unitOfWork.Repository<Offer>()
    .GetAllAsNoTracking(x => x.State == State.NotDeleted)
    .Include(x => x.Lk_DiscountType)
    .Include(x => x.Lk_DiscountMethod)
    .Include(x => x.ServiceProvider)
    .Include(x => x.Category)
    .Include(x => x.SubCategory)
    .OrderByDescending(x => x.CreatedOn)
    .ApplyFilters(filterConditions, request.PageNumber, request.PageSize, ref _ResponseDTO)
    .ToListAsync();
            int timeZoneOffset = _configuration.GetValue<int>("GlobalSettings:TimeDifference");

            DateTime nationalDateTime = DateTime.Now.AddHours(timeZoneOffset);
            var currentDate = DateOnly.FromDateTime(nationalDateTime);
            var currentTime = TimeOnly.FromDateTime(nationalDateTime);

            var Offers = offers.Select(x => new GetOfferDTO
            {
                Id = x.Id,
                NameAr = x.NameAr,
                NameEN = x.NameEN,
                DescriptionAEn = x.DescriptionAEn,
                DescriptionAr = x.DescriptionAr,
                ConditionsAr = x.ConditionsAr ?? "",
                ConditionsEn = x.ConditionsEn ?? "",
                Code = x.Code,
                SubCategoryNameAr = x.SubCategory.NameAr,
                SubCategoryNameEn = x.SubCategory.NameEn,
                CategoryNameAr = x.Category.NameAr,
                CategoryNameEn = x.Category.NameEn,
                DiscountAmount = x.DiscountAmount,
                ServiceProviderNameAr = x.ServiceProvider.NameAr,
                ServiceProviderNameEn = x.ServiceProvider.NameEn,
                StartDate = x.StartDate,
                StartTime = x.StartTime,
                EndDate = x.EndDate,
                EndTime = x.EndTime,
                imagePath = x.ImagePth,
                imagePathServiceProvider = x.ServiceProvider.Image ?? "",
                Lk_DiscountMethodAr = x.Lk_DiscountMethod?.NameAr ?? "",
                Lk_DiscountMethodEn = x.Lk_DiscountMethod?.NameEn ?? "",
                Lk_DiscountTypeAr = x.Lk_DiscountType?.NameAr ?? "",
                Lk_DiscountTypeEn = x.Lk_DiscountType?.NameEn ?? "",
                Other = x.Other,

                Status = ((x.StartDate + x.StartTime) <= nationalDateTime && (x.EndDate + x.EndTime) >= nationalDateTime)
                    ? Status.IsPublish
                    : Status.IsDrafft
            }).ToList();


            if (Offers.Count() <= 0)
            {
                return _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.NotFound] ?? "Error Localizer");
            }

            return _responseHelper.RetrievedSuccessfully(Offers, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "Error Localizer", ref _ResponseDTO);
        }

    }


}
