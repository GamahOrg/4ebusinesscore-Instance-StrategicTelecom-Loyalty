

namespace _4EPlatform.Application.Features.Categories.Command.PostCategory
{
    public class PostCategoryCommandHandler : IRequestHandler<PostCategoryCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hosting;
        private readonly IResponseHelper _responseHelper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public PostCategoryCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hosting, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hosting = hosting;
            _responseHelper = responseHelper;
        }
        public async Task<ResponseDTO> Handle(PostCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new PostCategoryValidation(_unitOfWork, _stringLocalizer);
                var validation = validator.Validate(request.PostCategoryPram);

                if (!validation.IsValid)
                    return _responseHelper.FailedToSave(string.Join("-", validation.Errors.Select(n => n.ErrorMessage).ToList()));

                var categoryMapped = _mapper.Map<Category>(request.PostCategoryPram);
                if (request.PostCategoryPram.IconImage is not null)
                {
                    var file = await Upload.UploadImage(request.PostCategoryPram.IconImage, _hosting, "CategoriesImage/" + Guid.NewGuid() + "Attachments");
                    if (!file.isSuccess)
                    {
                        return _responseHelper.SavedSuccessfully(file.Message);

                    }
                    categoryMapped.Iconpath = file.Message;
                }
                await _unitOfWork.Repository<Category>().AddAsync(categoryMapped);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Commit();
                return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.AddSuccessfully] ?? "Error Localizer");
            }
            catch (Exception ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;
                return _responseHelper.FailedToSave(message);

            }

        }
    }
}
