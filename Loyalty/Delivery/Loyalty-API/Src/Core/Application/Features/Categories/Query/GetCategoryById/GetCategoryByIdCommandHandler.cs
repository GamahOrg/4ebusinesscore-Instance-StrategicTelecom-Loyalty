namespace _4EPlatform.Application.Features.Categories.Query.GetCategoryById
{
    public class GetCategoryByIdCommandHandler : IRequestHandler<GetCategoryByIdCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public GetCategoryByIdCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
        }
        public async Task<ResponseDTO> Handle(GetCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            var category = _unitOfWork.Repository<Category>().GetAllAsNoTracking(x => x.Id == request.Id)
                                            .Select(x => new GetCategoryByIdDto
                                            {

                                                NameAr = x.NameAr,

                                                DescriptionAEn = x.DescriptionAEn,
                                                DescriptionAr = x.DescriptionAr,
                                                Iconpath = x.Iconpath,
                                                NameEn = x.NameEn,
                                                SubCategories = x.SubCategories.Select(t => new SubCategoryDto
                                                {
                                                    Id = t.Id,
                                                    NameAr = t.NameAr,
                                                    NameEn = t.NameEn,

                                                }).ToList()
                                            }).FirstOrDefault();
            if (category is null)
                return _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound] ?? "Error Localizer");

            return _responseHelper.RetrievedSuccessfully(category, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "Error Localizer");
        }
    }
}
