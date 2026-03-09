namespace _4EPlatform.Application.Features.SubCategories.Validations
{
    public class PostSubCategoryValidation : AbstractValidator<PostSubCategoryCommand>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public PostSubCategoryValidation(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            RuleFor(m => m.DTO.NameAr)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.NameArabicMustNotEmpty] ?? "Error Localizer")
                 .NotNull().WithMessage(stringLocalizer[SharedResourceKeys.NameArabicNotNULL] ?? "Error Localizer")
                 .Matches(@"^[\u0600-\u06FF\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInArabicMustBeWrittenInArabicAAndCanContainNumbers] ?? "Error Localizer");

            RuleFor(m => m.DTO.NameEn)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishMustNotEmpty] ?? "Error Localizer")
                .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishNotNULL] ?? "Error Localizer")
                .Matches(@"^[A-Za-z\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInEnglishMustBeWrittenInEnglishAndCanContainNumbers] ?? "Error Localizer");

            RuleFor(x => x)
                .Must(IsUniqueNameCombination)
                .WithMessage(_stringLocalizer[SharedResourceKeys.TheCombinationOfNameArAndNameEnMustBeUnique] ?? "Error Localizer");
            _stringLocalizer = stringLocalizer;
        }
        private bool IsUniqueNameCombination(PostSubCategoryCommand command)
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
            var existingLibrary = repository.GetAllAsTracking(x =>
     (x.NameAr == command.DTO.NameAr || x.NameEn == command.DTO.NameEn) &&
     x.State != State.Deleted).FirstOrDefault();

            return existingLibrary == null;
        }
    }
}
