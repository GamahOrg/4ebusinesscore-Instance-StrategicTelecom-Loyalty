using _4EPlatform.Application.Features.ServiceProviderFeature.Query.GetAll;

namespace _4EPlatform.Application.Features.ServiceProviderFeature.Validations
{
    public class GetAllServiceProviderValidation : AbstractValidator<GetAllServiceProviderQuery>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public GetAllServiceProviderValidation(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            RuleFor(x => x)
               .Must(checkCategory)
               .WithMessage(_stringLocalizer[SharedResourceKeys.ThisCategoryNotFound]);
            RuleFor(x => x)
              .Must(checkSubCategory)
              .WithMessage(_stringLocalizer[SharedResourceKeys.ThisSubCategoryNotFound]);
            _stringLocalizer = stringLocalizer;
        }
        private bool checkCategory(GetAllServiceProviderQuery query)
        {
            if (!string.IsNullOrEmpty(query.CategoryId.ToString()))
            {

                if (_unitOfWork == null)
                {
                    throw new InvalidOperationException("UnitOfWork is not initialized.");
                }

                var repository = _unitOfWork.Repository<Category>();
                if (repository == null)
                {
                    throw new InvalidOperationException("Repository is not initialized.");
                }
                var checkCategory = repository.GetAllAsTracking(x => x.Id == query.CategoryId
          &&
         x.State != State.Deleted).FirstOrDefault();

                return checkCategory != null;
            }
            return true;
        }
        private bool checkSubCategory(GetAllServiceProviderQuery query)
        {
            if (!string.IsNullOrEmpty(query.SubCategoryId.ToString()))
            {
                if (_unitOfWork == null)
                {
                    throw new InvalidOperationException("UnitOfWork is not initialized.");
                }

                var repository = _unitOfWork.Repository<SubCategory>();
                if (repository == null)
                {
                    throw new InvalidOperationException("Repository is not initialized.");
                }
                var checkCategory = repository.GetAllAsTracking(x => x.Id == query.SubCategoryId && x.CategoryId == query.CategoryId
          &&
         x.State != State.Deleted).FirstOrDefault();

                return checkCategory != null;
            }
            return true;
        }

    }
}
