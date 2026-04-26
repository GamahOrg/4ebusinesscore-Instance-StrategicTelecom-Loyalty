
namespace _4EPlatform.Application.Features.Lk_DiscountTypeFeature.LK_DiscountTypeValidation
{
    public class PostDiscountTypeValidator : AbstractValidator<PostDiscountTypeCommand>
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IUnitOfWork _unitOfWork;
        public PostDiscountTypeValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            RuleFor(m => m.DTO.NameAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.NameArabicMustNotEmpty])
                        .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.NameArabicNotNULL])
                        .Matches(@"^[\u0600-\u06FF\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInArabicMustBeWrittenInArabicAAndCanContainNumbers]);
            RuleFor(m => m.DTO.NameEn).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishMustNotEmpty])
                        .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishNotNULL])
                        .Matches(@"^[A-Za-z\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInEnglishMustBeWrittenInEnglishAndCanContainNumbers]);
            RuleFor(x => x)
            .Must(IsUniqueNameCombination)
            .WithMessage(_stringLocalizer[SharedResourceKeys.TheCombinationOfNameArAndNameEnMustBeUnique]);
        }




        private bool IsUniqueNameCombination(PostDiscountTypeCommand command)
        {

            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("UnitOfWork is not initialized.");
            }

            var repository = _unitOfWork.Repository<Lk_DiscountType>();
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
