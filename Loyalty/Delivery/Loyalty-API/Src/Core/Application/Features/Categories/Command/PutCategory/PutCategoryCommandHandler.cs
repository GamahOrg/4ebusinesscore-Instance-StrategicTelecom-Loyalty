namespace _4EPlatform.Application.Features.Categories.Command.PutCategory
{
    public class PutCategoryCommandHandler : IRequestHandler<PutCategoryCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hosting;
        private readonly IResponseHelper _responseHelper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public PutCategoryCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hosting, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hosting = hosting;
            _responseHelper = responseHelper;
        }

        public async Task<ResponseDTO> Handle(PutCategoryCommand request, CancellationToken cancellationToken)
        {

            var validator = new PutCategoryValidation(_unitOfWork, _stringLocalizer);
            var validation = validator.Validate(request.PutCategoryPram);

            if (!validation.IsValid)
                return _responseHelper.FailedToSave(string.Join("-", validation.Errors.Select(n => n.ErrorMessage).ToList()));

            var editCategory = _unitOfWork.Repository<Category>().GetAllAsTracking(x => x.Id == request.PutCategoryPram.Id).FirstOrDefault();
            if (editCategory == null)
            {
                return _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound] ?? "Error Localizer");
            }
            var causeMapped = _mapper.Map(request.PutCategoryPram, editCategory);
            if (request.PutCategoryPram.SubCategoryPram.Any())
            {
                var SubCategories = editCategory.SubCategories.ToList();
                foreach (var item in SubCategories)
                {
                    if (!request.PutCategoryPram.SubCategoryPram.Any(x => x.Id == item.Id))
                    {
                        item.State = State.Deleted;
                    }
                }
                foreach (var item in request.PutCategoryPram.SubCategoryPram)
                {
                    if (!SubCategories.Where(x => x.Id == item.Id).Any())
                    {
                        var sub = new SubCategory
                        {
                            NameAr = item.NameAr,
                            NameEn = item.NameEn,

                        };
                        editCategory.SubCategories.Add(sub);
                    }
                    else
                    {
                        var edit = SubCategories.Where(x => x.Id == item.Id).FirstOrDefault();
                        var subMapped = _mapper.Map(item, edit);
                    }
                }

            }
            else
            {
                foreach (var item in editCategory.SubCategories)
                {
                    item.State = State.Deleted;
                }
            }
            if (request.PutCategoryPram.IconImage is not null)
            {
                var file = await Upload.UploadFiles(request.PutCategoryPram.IconImage, _hosting, "CategoriesImage/" + Guid.NewGuid() + "Attachments");
                editCategory.Iconpath = file;
            }
            await _unitOfWork.Repository<Category>().UpdateAsync(editCategory);
            await _unitOfWork.SaveChangesAsync();
            _unitOfWork.Commit();
            return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.UpdateSuccessfully] ?? "Error Localizer");
        }
    }
}
