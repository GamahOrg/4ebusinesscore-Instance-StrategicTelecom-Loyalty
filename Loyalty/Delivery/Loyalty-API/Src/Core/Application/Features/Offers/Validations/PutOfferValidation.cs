namespace _4EPlatform.Application.Features.Offers.Validations
{
    public class PutOfferValidation : AbstractValidator<PutOfferCommand>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public PutOfferValidation(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            RuleFor(m => m.DTO.NameAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.NameArabicMustNotEmpty] ?? "Error Localizer")
                        .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.NameArabicNotNULL] ?? "Error Localizer")
                        .Matches(@"^[\u0600-\u06FF\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInArabicMustBeWrittenInArabicAAndCanContainNumbers] ?? "Error Localizer");

            RuleFor(m => m.DTO.DescriptionAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionArabicMustNotEmpty] ?? "Error Localizer")
                        .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionArabicNotNULL] ?? "Error Localizer")
                        .Matches(@"^[\u0600-\u06FF\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInArabicMustBeWrittenInArabicAAndCanContainNumbers] ?? "Error Localizer");


            RuleFor(m => m.DTO.NameEN).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishMustNotEmpty] ?? "Error Localizer")
                        .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishNotNULL] ?? "Error Localizer")
                        .Matches(@"^[A-Za-z\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInEnglishMustBeWrittenInEnglishAndCanContainNumbers] ?? "Error Localizer");


            RuleFor(m => m.DTO.DescriptionAEn).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionArabicMustNotEmpty] ?? "Error Localizer")
                        .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionArabicNotNULL] ?? "Error Localizer")
                        .Matches(@"^[A-Za-z\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionInEnglishMustBeWrittenInEnglishAndCanContainNumbers] ?? "Error Localizer");




            RuleFor(x => x.DTO.StartTime)
            .Matches(@"^(?:[01]\d|2[0-3]):[0-5]\d:[0-5]\d$")
            .WithMessage(_stringLocalizer[SharedResourceKeys.StartTimeFormat] ?? "Error Localizer");

            RuleFor(x => x.DTO.EndTime)
                .Matches(@"^(?:[01]\d|2[0-3]):[0-5]\d:[0-5]\d$")
                .WithMessage(_stringLocalizer[SharedResourceKeys.EndTimeFormat] ?? "Error Localizer");

            RuleFor(x => x.DTO.StartDate)
     .Must(date => date == DateTime.ParseExact(date.ToString("dd-MM-yyyy"), "dd-MM-yyyy", null))
     .WithMessage("Date must be in the format dd-MM-yyyy.");

            RuleFor(x => x.DTO.EndDate)
       .Must(date => date == DateTime.ParseExact(date.ToString("dd-MM-yyyy"), "dd-MM-yyyy", null))
       .WithMessage("Date must be in the format dd-MM-yyyy.");


            RuleFor(x => x.DTO.DiscountAmount)
    .Must(amount =>
    {
        var safeAmount = string.IsNullOrWhiteSpace(amount) ? "0" : amount;
        return decimal.TryParse(safeAmount, out var value) && value >= 0 && value <= 100;
    })
    .WithMessage(_stringLocalizer[SharedResourceKeys.DiscountAmountMustBeNumberBetweenOneToHundred] ?? "Error Localizer");


            RuleFor(x => x)
                .Must(IsUniqueNameCombination)
                .WithMessage(_stringLocalizer[SharedResourceKeys.TheCombinationOfNameArAndNameEnMustBeUnique] ?? "Error Localizer");
            RuleFor(x => x)
               .Must(checkCategory)
               .WithMessage("Category must be Found.");
            RuleFor(x => x)
              .Must(checkSubCategory)
              .WithMessage("SubCategory must be Found or not related with CategoryId.");
            RuleFor(x => x)
              .Must(checkServiceProvider)
              .WithMessage("ServiceProvider must be Found.");
            RuleFor(x => x);


        }
        private bool IsUniqueNameCombination(PutOfferCommand command)
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
     (x.NameAr == command.DTO.NameAr || x.NameEN == command.DTO.NameEN) &&
     x.State != State.Deleted && x.Id != command.Id).FirstOrDefault();

            return existingLibrary == null;
        }
        private bool checkCategory(PutOfferCommand command)
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
            var checkCategory = repository.GetAllAsTracking(x => x.Id == command.DTO.CategoryId
      &&
     x.State != State.Deleted).FirstOrDefault();

            return checkCategory != null;
        }
        private bool checkSubCategory(PutOfferCommand command)
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
            var checkCategory = repository.GetAllAsTracking(x => x.Id == command.DTO.SubCategoryId && x.CategoryId == command.DTO.CategoryId
      &&
     x.State != State.Deleted).FirstOrDefault();

            return checkCategory != null;
        }
        private bool checkServiceProvider(PutOfferCommand command)
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
            var checkCategory = repository.GetAllAsTracking(x => x.Id == command.DTO.ServiceProviderId
      &&
     x.State != State.Deleted).FirstOrDefault();

            return checkCategory != null;
        }



    }
}
