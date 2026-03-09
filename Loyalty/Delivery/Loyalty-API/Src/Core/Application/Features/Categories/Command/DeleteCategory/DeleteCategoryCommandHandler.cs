

namespace _4EPlatform.Application.Features.Categories.Command.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public DeleteCategoryCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
        }
        public async Task<ResponseDTO> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var Category = _unitOfWork.Repository<Category>().GetAllAsTracking(x => x.Id == request.Id).FirstOrDefault();
            if (Category == null)
                return _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound] ?? "Error Localizer");
            Category.State = State.Deleted;
            foreach (var item in Category.SubCategories)
            {
                item.State = State.Deleted;
            }
            await _unitOfWork.Repository<Category>().UpdateAsync(Category);
            await _unitOfWork.SaveChangesAsync();
            _unitOfWork.Commit();
            return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.DeleteSuccessfully] ?? "Error Localizer");
        }
    }
}
