namespace _4EPlatform.Application.Features.ServiceProviderFeature.Validations
{
    public class PutServiceProviderValidation : AbstractValidator<PutServiceProviderCommand>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public PutServiceProviderValidation(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            RuleFor(m => m.DTO.TitleAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.TitleArabickMustNotEmpty] ?? "Error Localizer")
                  .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.TitleArabickMustNotNull] ?? "Error Localizer")
                  .Matches(@"^[\u0600-\u06FF\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.TitleArabicMustBeWrittenInArabic] ?? "Error Localizer");

            RuleFor(m => m.DTO.NameAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.NameArabicMustNotEmpty] ?? "Error Localizer")
                        .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.NameArabicNotNULL] ?? "Error Localizer")
                        .Matches(@"^[\u0600-\u06FF\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInArabicMustBeWrittenInArabicAAndCanContainNumbers] ?? "Error Localizer");

            RuleFor(m => m.DTO.DescriptionAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionArabicMustNotEmpty] ?? "Error Localizer")
                        .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionArabicNotNULL] ?? "Error Localizer")
                        .Matches(@"^[\u0600-\u06FF\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionInArabicMustBeWrittenInArabicAAndCanContainNumbers] ?? "Error Localizer");


            RuleFor(m => m.DTO.NameEn).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishMustNotEmpty] ?? "Error Localizer")
                        .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishNotNULL] ?? "Error Localizer")
                        .Matches(@"^[A-Za-z\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInEnglishMustBeWrittenInEnglishAndCanContainNumbers] ?? "Error Localizer");


            RuleFor(m => m.DTO.DescriptionAEn).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionEnglishMustNotEmpty] ?? "Error Localizer")
                        .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionEnglishNotNULL])
                        .Matches(@"^[A-Za-z\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionInEnglishMustBeWrittenInEnglishAndCanContainNumbers] ?? "Error Localizer");
            RuleFor(m => m.DTO.TitleEn).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.TitleEnglishMustNotEmpty] ?? "Error Localizer")
         .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.TitleEnglishMustNotNull] ?? "Error Localizer")
                     .Matches(@"^[A-Za-z\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.TitleEnglishMustBeWrittenInEnglish] ?? "Error Localizer");










            RuleFor(x => x)
                .Must(IsUniqueNameCombination)
                .WithMessage(_stringLocalizer[SharedResourceKeys.TheCombinationOfNameArAndNameEnMustBeUnique] ?? "Error Localizer");
        }
        private bool IsUniqueNameCombination(PutServiceProviderCommand command)
        {

            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("UnitOfWork is not initialized.");
            }

            var repository = _unitOfWork.Repository<ServiceProvider>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }
            var existingLibrary = repository.GetAllAsTracking(x =>
     (x.NameAr == command.DTO.NameAr || x.NameEn == command.DTO.NameEn) &&
     x.State != State.Deleted && x.Id != command.Id).FirstOrDefault();

            return existingLibrary == null;
        }

    }
}
