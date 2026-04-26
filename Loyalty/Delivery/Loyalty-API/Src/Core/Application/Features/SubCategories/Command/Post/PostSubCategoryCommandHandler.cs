

namespace _4EPlatform.Application.Features.SubCategories.Command.Post
{
    public class PostSubCategoryCommandHandler : IRequestHandler<PostSubCategoryCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly IResponseHelper _responseHelper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public PostSubCategoryCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IMapper mapper, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            _responseHelper = responseHelper;
        }
        public async Task<ResponseDTO> Handle(PostSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new PostSubCategoryValidation(_unitOfWork, _stringLocalizer);
            var validation = validator.Validate(request);

            if (!validation.IsValid)
                return _responseHelper.FailedToSave(string.Join("-", validation.Errors.Select(n => n.ErrorMessage).ToList()));
            var category = _unitOfWork.Repository<Category>().GetAllAsNoTracking(x => x.Id == request.DTO.CategoryId).FirstOrDefault();

            if (category == null)
                _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);

            var subCategoryMapped = _mapper.Map<SubCategory>(request.DTO);


            await _unitOfWork.Repository<SubCategory>().AddAsync(subCategoryMapped);
            await _unitOfWork.SaveChangesAsync();
            _unitOfWork.Commit();
            return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.AddSuccessfully]);
        }
    }
}
