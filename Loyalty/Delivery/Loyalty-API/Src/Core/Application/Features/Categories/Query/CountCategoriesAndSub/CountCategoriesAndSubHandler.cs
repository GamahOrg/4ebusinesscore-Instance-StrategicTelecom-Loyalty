namespace _4EPlatform.Application.Features.Categories.Query.CountCategoriesAndSub
{
    internal class CountCategoriesAndSubHandler : IRequestHandler<CountCategoriesAndSubQuery, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        public ResponseDTO _ResponseDTO;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public CountCategoriesAndSubHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
            _ResponseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO> Handle(CountCategoriesAndSubQuery request, CancellationToken cancellationToken)
        {


            var CountCategories = await _unitOfWork.Repository<Category>().GetAllAsNoTracking(x => x.State == State.NotDeleted).CountAsync();

            if (CountCategories <= 0)
            {
                return _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.NotFound] ?? "Error Localizer");
            }
            var CountSubCategories = await _unitOfWork.Repository<SubCategory>().GetAllAsNoTracking(x => x.State == State.NotDeleted).CountAsync();
            var counts = new CountCategoriesAndSubDTO()
            {
                CountCategories = CountCategories,
                SubCountCategories = CountSubCategories



            };
            _ResponseDTO.Result = counts;
            return _responseHelper.RetrievedSuccessfully(counts, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "Error Localizer");
        }

    }
}
