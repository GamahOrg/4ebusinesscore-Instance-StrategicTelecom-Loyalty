
namespace _4EPlatform.Application.Features.Lk_DiscountTypeFeature.Command.Post
{
    public class PostDiscountTypeCommandHandler : IRequestHandler<PostDiscountTypeCommand, ResponseDTO>
    {
        private readonly IResponseHelper _responseHelper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public PostDiscountTypeCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IResponseHelper responseHelper, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _stringLocalizer = stringLocalizer;
            _responseHelper = responseHelper;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> Handle(PostDiscountTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new PostDiscountTypeValidator(_unitOfWork, _stringLocalizer);
            var validation = validator.Validate(request);
            if (!validation.IsValid)
                return _responseHelper.FailedToSave(string.Join("-", validation.Errors.Select(n => n.ErrorMessage).ToList()));

            var lkDiscount = await _unitOfWork.Repository<Lk_DiscountType>().GetAllAsNoTracking(e => e.NameAr == request.DTO.NameAr || e.NameEn == request.DTO.NameEn).FirstOrDefaultAsync();


            var lkDiscountMap = _mapper.Map<Lk_DiscountType>(request.DTO);
            lkDiscountMap.IsSeed = false;
            await _unitOfWork.Repository<Lk_DiscountType>().AddAsync(lkDiscountMap);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.AddSuccessfully] ?? "Error Localizations");
        }
    }
}