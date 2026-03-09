

namespace _4EPlatform.Application.Features.KPIs
{
    public class KPIsHandler : IRequestHandler<KPIsQuery, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        public ResponseDTO _ResponseDTO;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public KPIsHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
            _ResponseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO> Handle(KPIsQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var startOfNextMonth = startOfMonth.AddMonths(1);

            var CountAvailableOffers = await _unitOfWork.Repository<Offer>().GetAllAsNoTracking(x => x.State == State.NotDeleted && x.EndDate >= DateTime.Now).CountAsync();



            var offersActivelyMonthly = await _unitOfWork.Repository<Offer>()
                .GetAllAsNoTracking(x => x.State == State.NotDeleted &&
                     x.EndDate >= now && // still valid
                    x.StartDate < startOfNextMonth && // started before next month
                    x.EndDate >= startOfMonth // ends in or after current month
                )
                .CountAsync();

            var NumberDiscountMethodOffers = await _unitOfWork.Repository<Lk_DiscountMethod>().GetAllAsNoTracking(x => x.State == State.NotDeleted)

              .Select(item => new LookupIndicatorDTO
              {
                  LookupId = item.Id,
                  LookupName = item.NameAr + "-" + item.NameEn,
                  offerCount = item.Offers.Count()

              })

              .ToListAsync();
            var NumberDiscountTypeOffers = await _unitOfWork.Repository<Lk_DiscountType>().GetAllAsNoTracking(x => x.State == State.NotDeleted)

               .Select(item => new LookupIndicatorDTO
               {
                   LookupId = item.Id,
                   LookupName = item.NameAr + "-" + item.NameEn,
                   offerCount = item.Offers.Count()

               })

               .ToListAsync();
            var CountServiceProviderLikes = await _unitOfWork.Repository<ServiceProviderLike>().GetAllAsNoTracking(x => x.State == State.NotDeleted).CountAsync();
            var CountOfferLikeUsed = await _unitOfWork.Repository<OfferLikeUsed>().GetAllAsNoTracking(x => x.State == State.NotDeleted && x.OfferType == OfferType.Like).CountAsync();

            var CountCategories = await _unitOfWork.Repository<Category>().GetAllAsNoTracking(x => x.State == State.NotDeleted).CountAsync();

            var CountSubCategories = await _unitOfWork.Repository<SubCategory>().GetAllAsNoTracking(x => x.State == State.NotDeleted).CountAsync();
            var countCatsAndSubs = new CountCategoriesAndSubDTO()
            {
                CountCategories = CountCategories,
                SubCountCategories = CountSubCategories



            };
            var CountServiceProvider = await _unitOfWork.Repository<ServiceProvider>().GetAllAsNoTracking(x => x.State == State.NotDeleted).CountAsync();
            var Offers = await _unitOfWork.Repository<Offer>().GetAllAsNoTracking(x => x.State == State.NotDeleted).CountAsync();


            var SumAmount = await _unitOfWork.Repository<Offer>().GetAllAsNoTracking(x => x.State == State.NotDeleted).SumAsync(x => x.DiscountAmount);
            var offerT = Offers != 0 ? Offers : 0;

            var amount = SumAmount / offerT;
            _ResponseDTO.Result = amount;



            var DTO = new KPIsDTO()
            {
                CountOfferLikes = CountOfferLikeUsed,
                CountServiceProviderLikes = CountServiceProviderLikes,
                CountOfOffersAvailable = CountAvailableOffers,
                AverageDiscountAmount = CountAvailableOffers,
                CountOffersActivelyMonthly = offersActivelyMonthly,
                countCategoriesAndSubDTO = countCatsAndSubs,
                CountOfferInDiscountType = NumberDiscountTypeOffers,
                CountOfferInDiscountMethod = NumberDiscountMethodOffers,
                CountServiceProvider = CountServiceProvider


            };
            return _responseHelper.RetrievedSuccessfully(DTO, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "Error Localizer");
        }

    }

}

