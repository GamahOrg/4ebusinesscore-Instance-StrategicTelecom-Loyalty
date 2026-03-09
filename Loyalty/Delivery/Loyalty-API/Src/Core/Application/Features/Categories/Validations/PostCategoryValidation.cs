

namespace _4EPlatform.Application.Features.Categories.Validations
{
    public class PostCategoryValidation : AbstractValidator<PostCategoryPram>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public PostCategoryValidation(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            RuleFor(m => m.NameAr)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.NameArabicMustNotEmpty] ?? "Error Localizer")
                .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.NameArabicNotNULL] ?? "Error Localizer")
                .Matches(@"^[\u0600-\u06FF\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInArabicMustBeWrittenInArabicAAndCanContainNumbers] ?? "Error Localizer");

            RuleFor(m => m.DescriptionAr)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionArabicMustNotEmpty] ?? "Error Localizer")
                .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionArabicNotNULL] ?? "Error Localizer")
                .Matches(@"^[\u0600-\u06FF\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionInArabicMustBeWrittenInArabicAAndCanContainNumbers] ?? "Error Localizer");




            RuleFor(m => m.NameEn)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishMustNotEmpty] ?? "Error Localizer")
                .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishNotNULL] ?? "Error Localizer")
                .Matches(@"^[A-Za-z\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInEnglishMustBeWrittenInEnglishAndCanContainNumbers] ?? "Error Localizer");

            RuleFor(m => m.DescriptionAEn)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionEnglishMustNotEmpty] ?? "Error Localizer")
                .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionEnglishNotNULL] ?? "Error Localizer")
                .Matches(@"^[A-Za-z\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionInEnglishMustBeWrittenInEnglishAndCanContainNumbers] ?? "Error Localizer");

            RuleForEach(m => m.SubCategories).ChildRules(sub =>
            {
                sub.RuleFor(x => x.NameAr)
                    .NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.SubcategoryNameArabicMustNotBeEmpty] ?? "Error Localizer")
                    .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.SubcategoryNameArabicCannotBeNull] ?? "Error Localizer")
                    .Matches(@"^[\u0600-\u06FF\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.SubcategoryNameInArabicMustBeWrittenInArabicAndCanContainNumbers] ?? "Error Localizer");

                sub.RuleFor(x => x.NameEn)
                    .NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.SubcategoryNameEnglishMustNotBeEmpty] ?? "Error Localizer")
                    .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.SubcategoryNameEnglishCannotBeNull] ?? "Error Localizer")
                    .Matches(@"^[A-Za-z\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.SubcategoryNameInEnglishMustBeWrittenInEnglishAndCanContainNumbers] ?? "Error Localizer");
            });

            RuleFor(x => x)
                .Must(IsUniqueNameCombination)
                .WithMessage(_stringLocalizer[SharedResourceKeys.TheCombinationOfNameArAndNameEnMustBeUnique] ?? "Error Localizer");
        }

        private bool IsUniqueNameCombination(PostCategoryPram command)
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
            var existingLibrary = repository.GetAllAsTracking(x =>
     (x.NameAr == command.NameAr || x.NameEn == command.NameEn) &&
     x.State != State.Deleted).FirstOrDefault();

            return existingLibrary == null;
        }
    }

}


