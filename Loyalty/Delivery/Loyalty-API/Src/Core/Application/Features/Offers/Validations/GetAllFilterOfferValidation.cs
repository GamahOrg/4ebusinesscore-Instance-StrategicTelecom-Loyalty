namespace _4EPlatform.Application.Features.Offers.Validations
{
    public class GetAllFilterOfferValidation : AbstractValidator<GetAllOfferQuery>
    {
        private IStringLocalizer _stringLocalizer;


        public GetAllFilterOfferValidation(IStringLocalizer stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

        }
    }
}
