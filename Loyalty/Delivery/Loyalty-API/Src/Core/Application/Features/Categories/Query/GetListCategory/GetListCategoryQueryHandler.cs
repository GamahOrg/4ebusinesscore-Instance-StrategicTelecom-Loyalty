

namespace _4EPlatform.Application.Features.Categories.Query.GetListCategory
{
    public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, ResponseDTO>
    {
        #region Fields
        private readonly IUnitOfWork _repository;
        private IResponseHelper _responseDTO;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public GetListCategoryQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork repository, IMapper mapper, IResponseHelper response)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;
            _responseDTO = response;
            _mapper = mapper;
        }
        #endregion
        #region Handler
        public async Task<ResponseDTO> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            var listCategory = await _repository.Repository<Category>().GetAllAsNoTracking(x => x.State == State.NotDeleted)
                .OrderByDescending(e => e.CreatedOn)
                .ToListAsync();

            if (!listCategory.Any())
                return _responseDTO.NotFound(_stringLocalizer[SharedResourceKeys.NotFound] ?? "Error Localizer");

            var listCategoryMapper = _mapper.Map<List<GetCategoryDTO>>(listCategory);

            return _responseDTO.RetrievedSuccessfully(listCategoryMapper, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "Error Localizer");
        }
        #endregion
    }
}


