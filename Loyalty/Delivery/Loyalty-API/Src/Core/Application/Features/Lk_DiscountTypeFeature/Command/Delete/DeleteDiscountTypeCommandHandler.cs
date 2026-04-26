namespace _4EPlatform.Application.Features.Lk_DiscountTypeFeature.Command.Delete
{
    public class DeleteDiscountTypeCommandHandler : IRequestHandler<DeleteDiscountTypeCommand, ResponseDTO>
    {
        private readonly IResponseHelper _responseHelper;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public DeleteDiscountTypeCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IResponseHelper responseHelper, IUnitOfWork unitOfWork)
        {
            _stringLocalizer = stringLocalizer;
            _responseHelper = responseHelper;
            _unitOfWork = unitOfWork;

        }

        public async Task<ResponseDTO> Handle(DeleteDiscountTypeCommand request, CancellationToken cancellationToken)
        {
            var lkDiscount = await _unitOfWork.Repository<Lk_DiscountType>().GetAllAsTracking(e => e.Id == request.Id).FirstOrDefaultAsync();
            if (lkDiscount == null)
                return _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);
            if (lkDiscount.IsSeed)
                return _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.CanNotDeleteAsItIsSeedByAdmin]);
            if (lkDiscount.Offers.Any(x => x.EndDate > DateTime.UtcNow))
                return _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.CanNotDeleteAsItHaveActiveOffers] ?? "Error Localizations");

            lkDiscount.State = State.Deleted;

            await _unitOfWork.Repository<Lk_DiscountType>().UpdateAsync(lkDiscount);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.DeleteSuccessfully] ?? "Error Localizations");
        }
    }
}

