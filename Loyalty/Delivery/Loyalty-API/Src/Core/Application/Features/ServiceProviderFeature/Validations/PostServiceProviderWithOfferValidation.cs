namespace _4EPlatform.Application.Features.ServiceProviderFeature.Validations
{
    public class PostServiceProviderWithOfferValidation : AbstractValidator<PostServiceProviderWitOfferCommand>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public PostServiceProviderWithOfferValidation(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;


            #region serviceProvidor

            RuleFor(m => m.DTO.TitleAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.TitleArabickMustNotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.TitleArabickMustNotNull])
                 .Matches(@"^[\u0600-\u06FF\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.TitleArabicMustBeWrittenInArabic]);


            RuleFor(m => m.DTO.NameAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.NameArabicMustNotEmpty])
                        .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.NameArabicNotNULL])
                        .Matches(@"^[\u0600-\u06FF\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInArabicMustBeWrittenInArabicAAndCanContainNumbers]);

            RuleFor(m => m.DTO.DescriptionAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionArabicMustNotEmpty])
                        .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionArabicNotNULL])
                        .Matches(@"^[\u0600-\u06FF\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionInArabicMustBeWrittenInArabicAAndCanContainNumbers]);


            RuleFor(m => m.DTO.NameEn).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishMustNotEmpty])
                        .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishNotNULL])
                        .Matches(@"^[A-Za-z\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInEnglishMustBeWrittenInEnglishAndCanContainNumbers]);


            RuleFor(m => m.DTO.DescriptionAEn).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionEnglishMustNotEmpty])
                        .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionArabicNotNULL])
                        .Matches(@"^[A-Za-z\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionInEnglishMustBeWrittenInEnglishAndCanContainNumbers]);


            RuleFor(m => m.DTO.TitleEn).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.TitleEnglishMustNotEmpty])
.NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.TitleEnglishMustNotNull])
            .Matches(@"^[A-Za-z\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.TitleEnglishMustBeWrittenInEnglish]);


            RuleFor(m => m.DTO.NameEn).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishMustNotEmpty])
                        .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishNotNULL])
                        .Matches(@"^[A-Za-z\s0-9._]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInEnglishMustBeWrittenInEnglishAndCanContainNumbers]);

            RuleFor(x => x.DTO.Image)
                .NotNull()
                .WithMessage(_stringLocalizer[SharedResourceKeys.ImageCanNotBeNull]);
            #endregion


            RuleFor(x => x.DTO.Website)
        .NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.Required])
        .Must(BeAValidUrl).WithMessage(_stringLocalizer[SharedResourceKeys.WebsiteMustBeAvailedURL]);





            #region offer


            RuleFor(m => m.DTO.offerDTO.NameAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.NameArabicMustNotEmpty])
                            .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.NameArabicNotNULL])
                            .Matches(@"^[\u0600-\u06FF\s\d]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInArabicMustBeWrittenInArabicAAndCanContainNumbers]);

            RuleFor(m => m.DTO.offerDTO.DescriptionAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionArabicMustNotEmpty])
                            .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionArabicNotNULL])
                            .Matches(@"^[\u0600-\u06FF\s\d]+$").WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionInArabicMustBeWrittenInArabicAAndCanContainNumbers]);


            RuleFor(m => m.DTO.offerDTO.NameEN).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishMustNotEmpty])
                            .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.NameEnglishNotNULL])
                            .Matches(@"^[A-Za-z\s\d]+$").WithMessage(_stringLocalizer[SharedResourceKeys.NameInEnglishMustBeWrittenInEnglishAndCanContainNumbers]);








            RuleFor(m => m.DTO.offerDTO.DescriptionAEn).NotEmpty().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionEnglishMustNotEmpty])
                    .NotNull().WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionEnglishNotNULL])
                    .Matches(@"^[A-Za-z\s\d]+$").WithMessage(_stringLocalizer[SharedResourceKeys.DescriptionInEnglishMustBeWrittenInEnglishAndCanContainNumbers]);

            RuleFor(x => x.DTO.offerDTO.StartTime)
                .Matches(@"^(?:[01]\d|2[0-3]):[0-5]\d:[0-5]\d$")
                .WithMessage(_stringLocalizer[SharedResourceKeys.StartTimeFormat]);

            RuleFor(x => x.DTO.offerDTO.EndTime)
                    .Matches(@"^(?:[01]\d|2[0-3]):[0-5]\d:[0-5]\d$")
                    .WithMessage(_stringLocalizer[SharedResourceKeys.EndTimeFormat]);

            RuleFor(x => x.DTO.offerDTO.ImagePth)
                    .NotNull()
                    .WithMessage(_stringLocalizer[SharedResourceKeys.ImageCanNotBeNull]);







            RuleFor(x => x.DTO.offerDTO.DiscountAmount)
        .Must(amount =>
        {
            var safeAmount = string.IsNullOrWhiteSpace(amount) ? "0" : amount;
            return decimal.TryParse(safeAmount, out var value) && value >= 0 && value <= 100;
        })
        .WithMessage(_stringLocalizer[SharedResourceKeys.DiscountAmountMustBeNumberBetweenOneToHundred]);


            #endregion






            RuleFor(x => x)
                    .Must(IsUniqueNameCombination)
                    .WithMessage(_stringLocalizer[SharedResourceKeys.TheCombinationOfNameArAndNameEnMustBeUnique]);
            RuleFor(x => x)
                  .Must(checkCategory)
                  .WithMessage("Category must be Found.");
            RuleFor(x => x)
                  .Must(checkSubCategory)
                  .WithMessage("SubCategory must be Found.");

            //RuleFor(x => x)
            //      .Must(checkLk_DiscountType)
            //      .WithMessage("Lk_DiscountType must be Found.");
            //RuleFor(x => x)
            //      .Must(checkLk_DiscountMethod)
            //      .WithMessage("Lk_DiscountMethod must be Found.");


        }
        private bool IsUniqueNameCombination(PostServiceProviderWitOfferCommand command)
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
            var existingServiceProvider = repository.GetAllAsTracking(x =>
        (x.NameAr == command.DTO.NameAr || x.NameEn == command.DTO.NameEn) &&
          x.State != State.Deleted).FirstOrDefault();

            return existingServiceProvider == null;
        }
        private bool checkCategory(PostServiceProviderWitOfferCommand command)
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
            var checkCategory = repository.GetAllAsTracking(x => x.Id == command.DTO.offerDTO.CategoryId
      &&
     x.State != State.Deleted).FirstOrDefault();

            return checkCategory != null;
        }
        private bool checkSubCategory(PostServiceProviderWitOfferCommand command)
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
            var checkCategory = repository.GetAllAsTracking(x => x.Id == command.DTO.offerDTO.SubCategoryId
      &&
     x.State != State.Deleted).FirstOrDefault();

            return checkCategory != null;
        }

        private bool checkLk_DiscountType(PostServiceProviderWitOfferCommand command)
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
            var checkCategory = repository.GetAllAsTracking(x => x.Id == command.DTO.offerDTO.Lk_DiscountTypeId
      &&
     x.State != State.Deleted).FirstOrDefault();

            return checkCategory != null;
        }
        private bool checkLk_DiscountMethod(PostServiceProviderWitOfferCommand command)
        {

            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("UnitOfWork is not initialized.");
            }

            var repository = _unitOfWork.Repository<Lk_DiscountMethod>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }
            var checkCategory = repository.GetAllAsTracking(x => x.Id == command.DTO.offerDTO.Lk_DiscountMethodId
      &&
     x.State != State.Deleted).FirstOrDefault();

            return checkCategory != null;
        }
        private bool BeAValidUrl(string website)
        {
            return Uri.TryCreate(website, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

    }
}



