namespace _4EPlatform.Application.Features.Lk_DiscountMethodFeature.Command.Delete
{
    public class DeleteDiscountMethodCommandHandler : IRequestHandler<DeleteDiscountMethodCommand, ResponseDTO>
    {
        private readonly IResponseHelper _responseHelper;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public DeleteDiscountMethodCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IResponseHelper responseHelper, IUnitOfWork unitOfWork)
        {
            _stringLocalizer = stringLocalizer;
            _responseHelper = responseHelper;
            _unitOfWork = unitOfWork;

        }

        public async Task<ResponseDTO> Handle(DeleteDiscountMethodCommand request, CancellationToken cancellationToken)
        {
            var lk_Discount = await _unitOfWork.Repository<Lk_DiscountMethod>().GetAllAsTracking(e => e.Id == request.Id).FirstOrDefaultAsync();
            if (lk_Discount == null)
                return _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);
            if (lk_Discount.IsSeed)
                return _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.CanNotDeleteAsItIsSeedByAdmin]);

            if (lk_Discount.Offers.Any(x => x.EndDate > DateTime.UtcNow))
                return _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.CanNotDeleteAsItHaveActiveOffers] ?? "Error Localizations");

            lk_Discount.State = State.Deleted;

            await _unitOfWork.Repository<Lk_DiscountMethod>().UpdateAsync(lk_Discount);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.DeleteSuccessfully]);
        }
    }
}

