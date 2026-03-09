namespace _4EPlatform.Application.Features.Offers.Command.Delete
{
    public class DeleteOfferHandler : IRequestHandler<DeleteOfferCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public DeleteOfferHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
        }
        public async Task<ResponseDTO> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {


            var Offer = _unitOfWork.Repository<Offer>().GetAllAsTracking(x => x.Id == request.Id && x.State == State.NotDeleted).FirstOrDefault();
            if (Offer == null)
            {
                return _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.NotFound]);
            }
            if (!string.IsNullOrEmpty(Offer.ImagePth))
                Upload.DeleteFile(Offer.ImagePth);

            Offer.State = State.Deleted;

            await _unitOfWork.Repository<Offer>().UpdateAsync(Offer);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();


            return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.DeleteSuccessfully]);
        }

    }
}
