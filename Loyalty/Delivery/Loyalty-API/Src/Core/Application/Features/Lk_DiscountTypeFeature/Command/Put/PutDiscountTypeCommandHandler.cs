namespace _4EPlatform.Application.Features.Lk_DiscountTypeFeature.Command.Put
{
    public class PutDiscountTypeCommandHandler : IRequestHandler<PutDiscountTypeCommand, ResponseDTO>
    {
        private readonly IResponseHelper _responseHelper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public PutDiscountTypeCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IResponseHelper responseHelper, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _stringLocalizer = stringLocalizer;
            _responseHelper = responseHelper;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> Handle(PutDiscountTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new PutDiscountTypeValidator(_unitOfWork, _stringLocalizer);
            var validation = validator.Validate(request);
            if (!validation.IsValid)
                return _responseHelper.FailedToSave(string.Join("-", validation.Errors.Select(n => n.ErrorMessage).ToList()));

            var lkDiscount = await _unitOfWork.Repository<Lk_DiscountType>().GetAllAsTracking(e => e.Id == request.Id).FirstOrDefaultAsync();
            if (lkDiscount == null)
                return _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);
            if (lkDiscount.IsSeed)
                return _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.CanNotPutAsItIsSeedByAdmin]);

            var lkDiscountMap = _mapper.Map(request.DTO, lkDiscount);

            await _unitOfWork.Repository<Lk_DiscountType>().UpdateAsync(lkDiscountMap);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.UpdateSuccessfully]);
        }
    }
}

