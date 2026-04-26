
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Configuration;
using GetOfferDTO = _4EPlatform.Application.Features.Offers.Query.GetAll.GetOfferDTO;

namespace _4EPlatform.Application.Features.Offers.Query.GetByServiceProviderId
{
    public class GetOffersByServiceProviderIdQueryHandler : IRequestHandler<GetOffersByIServiceProviderQuery, ResponseDTO>
    {
        #region Fields
        private readonly IUnitOfWork _repository;
        private IResponseHelper _responseDTO;
        private readonly IMapper _mapper;
        public Guid _loggedInUserId;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IConfiguration _configuration;
        #endregion
        #region Constructor
        public GetOffersByServiceProviderIdQueryHandler(IConfiguration configuration, IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork repository, IMapper mapper, IResponseHelper response, IHttpContextAccessor _httpContextAccessor)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;
            _responseDTO = response;
            _mapper = mapper;
            _configuration = configuration;
            _loggedInUserId = LoggedInUserProvider.GetLoggedInUserId(_httpContextAccessor);
        }
        #endregion
        #region Handler
        public async Task<ResponseDTO> Handle(GetOffersByIServiceProviderQuery request, CancellationToken cancellationToken)
        {
            var ServiceProvider = await _repository.Repository<ServiceProvider>().GetAllAsNoTracking(e => e.Id == request.ServiceProviderId).FirstOrDefaultAsync();

            if (ServiceProvider == null)
                return _responseDTO.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);
            var offers = await _repository.Repository<Offer>().GetAllAsNoTracking(e => e.ServiceProviderId == request.ServiceProviderId)

                .ToListAsync();

            int timeZoneOffset = _configuration.GetValue<int>("GlobalSettings:TimeDifference");

            DateTime nationalDateTime = DateTime.Now.AddHours(timeZoneOffset);
            var currentDate = DateOnly.FromDateTime(nationalDateTime);
            var currentTime = TimeOnly.FromDateTime(nationalDateTime);
            var Offers = offers.Select(x => new GetOfferDTO
            {

                Id = x.Id,
                NameAr = x.NameAr ?? "",
                NameEN = x.NameEN ?? "",
                DescriptionAEn = x.DescriptionAEn ?? "",
                DescriptionAr = x.DescriptionAr ?? "",
                ConditionsEn = x.ConditionsEn ?? "",
                ConditionsAr = x.ConditionsAr ?? "",
                SubCategoryNameAr = x.SubCategory?.NameAr ?? "",
                SubCategoryNameEn = x.SubCategory?.NameEn ?? "",
                CategoryNameAr = x.Category?.NameAr ?? "",
                CategoryNameEn = x.Category?.NameEn ?? "",
                DiscountAmount = x.DiscountAmount,
                ServiceProviderNameAr = x.ServiceProvider?.NameAr ?? "",
                ServiceProviderNameEn = x.ServiceProvider?.NameEn ?? "",
                StartDate = x.StartDate,
                StartTime = x.StartTime,
                Code = x.Code,
                DiscountLink = x.DiscountLink,
                EndDate = x.EndDate,
                EndTime = x.EndTime,
                imagePath = x.ImagePth,
                Lk_DiscountMethodAr = x.Lk_DiscountMethod != null ? x.Lk_DiscountMethod.NameAr : "",
                Lk_DiscountMethodEn = x.Lk_DiscountMethod != null ? x.Lk_DiscountMethod.NameEn : "",
                Lk_DiscountTypeAr = x.Lk_DiscountType != null ? x.Lk_DiscountType.NameAr : "",
                Lk_DiscountTypeEn = x.Lk_DiscountType != null ? x.Lk_DiscountType.NameEn : "",
                Other = x.Other,
                Status = ((x.StartDate + x.StartTime) <= nationalDateTime && (x.EndDate + x.EndTime) >= nationalDateTime)
                  ? Status.IsPublish
                  : Status.IsDrafft


            }).ToList();





            return _responseDTO.RetrievedSuccessfully(Offers, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully]);
        }
        #endregion

    }
}

