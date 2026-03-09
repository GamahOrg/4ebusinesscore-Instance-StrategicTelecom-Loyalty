

namespace _4EPlatform.Application.Features.Lk_OfferType.Command.Delete
{
    public class DeleteLK_OfferTypeCommandHandler : IRequestHandler<DeleteLK_OfferTypeCommand, ResponseDTO>
    {
        private readonly IResponseHelper _responseHelper;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public DeleteLK_OfferTypeCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IResponseHelper responseHelper, IUnitOfWork unitOfWork)
        {
            _stringLocalizer = stringLocalizer;
            _responseHelper = responseHelper;
            _unitOfWork = unitOfWork;

        }

        public async Task<ResponseDTO> Handle(DeleteLK_OfferTypeCommand request, CancellationToken cancellationToken)
        {
            var LK_OfferType = await _unitOfWork.Repository<LK_OfferType>().GetAllAsTracking(e => e.Id == request.Id).FirstOrDefaultAsync();
            if (LK_OfferType == null)
                return _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);


            LK_OfferType.State = State.Deleted;

            await _unitOfWork.Repository<LK_OfferType>().UpdateAsync(LK_OfferType);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.DeleteSuccessfully]);
        }

    }
}
