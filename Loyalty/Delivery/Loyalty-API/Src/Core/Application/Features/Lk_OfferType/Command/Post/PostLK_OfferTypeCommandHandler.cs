using _4EPlatform.Application.Features.Validation;
using _4EPlatform.Domain.Models.Lookup;

namespace _4EPlatform.Application.Features.Command.Post
{
    public class PostLK_OfferTypeCommandHandler : IRequestHandler<PostLK_OfferTypeCommand, ResponseDTO>
    {
        private readonly IResponseHelper _responseHelper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public PostLK_OfferTypeCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IResponseHelper responseHelper, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _stringLocalizer = stringLocalizer;
            _responseHelper = responseHelper;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> Handle(PostLK_OfferTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new PostLK_OfferTypeValidator(_unitOfWork, _stringLocalizer);
            var validation = validator.Validate(request);
            if (!validation.IsValid)
                return _responseHelper.FailedToSave(string.Join("-", validation.Errors.Select(n => n.ErrorMessage).ToList()));

            var LK_OfferType = await _unitOfWork.Repository<LK_OfferType>().GetAllAsNoTracking(e => e.NameAr == request.DTO.NameAr || e.NameEn == request.DTO.NameEn).FirstOrDefaultAsync();


            var lkDiscountMap = _mapper.Map<LK_OfferType>(request.DTO);
            await _unitOfWork.Repository<LK_OfferType>().AddAsync(lkDiscountMap);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.AddSuccessfully]);
        }
    }

}

