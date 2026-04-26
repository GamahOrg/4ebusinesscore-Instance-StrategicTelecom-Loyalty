namespace _4EPlatform.Application.Features.Categories.Query.GetAllCategories
{
    public class GetAllCategoriesCommandHandler : IRequestHandler<GetAllCategoriesCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        public ResponseDTO _responseDTO;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public GetAllCategoriesCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
            _responseDTO = new ResponseDTO();
        }
        public async Task<ResponseDTO> Handle(GetAllCategoriesCommand request, CancellationToken cancellationToken)
        {



            var filterConditions = new Dictionary<Expression<Func<Category, bool>>, bool>();

            if (!string.IsNullOrEmpty(request.Search))
            {
                filterConditions.Add(x => x.NameAr == request.Search ||
                                          x.NameAr.Contains(request.Search) || x.NameEn == request.Search || x.NameEn.Contains(request.Search)
                                          , true);
            }


            var categories = _unitOfWork.Repository<Category>().GetAllAsNoTracking(x => x.State == State.NotDeleted)
                .OrderByDescending(x => x.CreatedOn)
                .ApplyFilters(filterConditions, request.PageNumber, request.PageSize, ref _responseDTO)

                                            .Select(x => new GetAllCategoriesDto
                                            {
                                                Id = x.Id,
                                                NameAr = x.NameAr,

                                                DescriptionAEn = x.DescriptionAEn,
                                                DescriptionAr = x.DescriptionAr,
                                                Iconpath = x.Iconpath,
                                                NameEn = x.NameEn,
                                                SubCategoriesCount = x.SubCategories.Count()
                                            }).ToList();
            if (!categories.Any())
                return _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);


            return _responseHelper.RetrievedSuccessfully(categories, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "Error Localizer", ref _responseDTO);
        }
    }
}
