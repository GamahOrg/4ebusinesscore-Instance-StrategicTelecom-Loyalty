namespace _4EPlatform.Application.Features.Lk_DiscountMethodFeature.LK_DiscountMethodValidation
{


    public class PutDiscountMethodValidator : AbstractValidator<PutDiscountMethodCommand>
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IUnitOfWork _unitOfWork;
        public PutDiscountMethodValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> stringLocalizer)
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




        private bool IsUniqueNameCombination(PutDiscountMethodCommand command)
        {

            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("UnitOfWork is not initialized.");
            }

            var repository = _unitOfWork.Repository<Offer>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }
            var existingLibrary = repository.GetAllAsTracking(x =>
     (x.NameAr == command.DTO.NameAr || x.NameEN == command.DTO.NameEn) &&
     x.State != State.Deleted && command.Id != x.Id).FirstOrDefault();

            return existingLibrary == null;
        }
    }
}


