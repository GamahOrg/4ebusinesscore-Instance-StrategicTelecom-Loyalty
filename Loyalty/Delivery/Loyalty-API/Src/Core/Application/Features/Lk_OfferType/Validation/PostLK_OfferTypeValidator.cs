

namespace _4EPlatform.Application.Features.Validation
{
    public class PostLK_OfferTypeValidator : AbstractValidator<PostLK_OfferTypeCommand>
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IUnitOfWork _unitOfWork;
        public PostLK_OfferTypeValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            RuleFor(m => m.DTO.NameAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.NameArabicMustNotEmpty])
                        .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.NameArabicNotNULL])
                        .Matches(@"^[\u0600-\u06FF\s\d]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInArabicMustBeWrittenInArabicAAndCanContainNumbers]);
            RuleFor(m => m.DTO.NameEn).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishMustNotEmpty])
                        .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishNotNULL])
                        .Matches(@"^[A-Za-z\s\d]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInEnglishMustBeWrittenInEnglishAndCanContainNumbers]);
            RuleFor(x => x)
            .Must(IsUniqueNameCombination)
            .WithMessage(_stringLocalizer[SharedResourceKeys.TheCombinationOfNameArAndNameEnMustBeUnique]);
        }




        private bool IsUniqueNameCombination(PostLK_OfferTypeCommand command)
        {

            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("UnitOfWork is not initialized.");
            }

            var repository = _unitOfWork.Repository<LK_OfferType>();
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

