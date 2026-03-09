

namespace _4EPlatform.Application.Features.Lk_OfferType.Command.Put
{
    public class PutLK_OfferTypeHandler : IRequestHandler<PutLk_OfferTypeCommand, ResponseDTO>
    {
        private readonly IResponseHelper _responseHelper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public PutLK_OfferTypeHandler(IStringLocalizer<SharedResource> stringLocalizer, IResponseHelper responseHelper, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _stringLocalizer = stringLocalizer;
            _responseHelper = responseHelper;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> Handle(PutLk_OfferTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new PutLK_OfferTypeValidator(_unitOfWork, _stringLocalizer);
            var validation = validator.Validate(request);
            if (!validation.IsValid)
                return _responseHelper.FailedToSave(string.Join("-", validation.Errors.Select(n => n.ErrorMessage).ToList()));

            var LKOfferType = await _unitOfWork.Repository<LK_OfferType>().GetAllAsTracking(e => e.Id == request.Id).FirstOrDefaultAsync();
            if (LKOfferType == null)
                return _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);

            var LKOfferTypeMap = _mapper.Map(request.DTO, LKOfferType);

            await _unitOfWork.Repository<LK_OfferType>().UpdateAsync(LKOfferTypeMap);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.UpdateSuccessfully]);
        }
    }
}
