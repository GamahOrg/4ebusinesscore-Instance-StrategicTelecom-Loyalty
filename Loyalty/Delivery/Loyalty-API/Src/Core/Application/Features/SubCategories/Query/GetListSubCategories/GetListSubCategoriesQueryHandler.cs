namespace _4EPlatform.Application.Features.SubCategories.Query.GetListSubCategories
{
    public class GetListSubCategoriesQueryHandler : IRequestHandler<GetListSubCategoriesQuery, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        public ResponseDTO _responseDTO;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public GetListSubCategoriesQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
            _responseDTO = new ResponseDTO();
        }
        public async Task<ResponseDTO> Handle(GetListSubCategoriesQuery request, CancellationToken cancellationToken)
        {

            var subCategories = await _unitOfWork.Repository<SubCategory>().GetAllAsNoTracking(x => x.CategoryId == request.CategoryId).Include(x => x.Offers)
                                    .Select(x => new GetListSubCategoriesDto
                                    {
                                        Id = x.Id,
                                        NameAr = x.NameAr,
                                        NameEn = x.NameEn,

                                        OfferCount = x.Offers.Count()
                                    }).ToListAsync();

            if (!subCategories.Any())
                return _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);

            if (request.Status != null)
            {

                var subCategoriesWithFilter = subCategories.Where(x => x.Status == request.Status).ToList();
                return _responseHelper.RetrievedSuccessfully(subCategoriesWithFilter, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully]);
            }
            subCategories.ToList();

            return _responseHelper.RetrievedSuccessfully(subCategories, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully]);
        }
    }
}
