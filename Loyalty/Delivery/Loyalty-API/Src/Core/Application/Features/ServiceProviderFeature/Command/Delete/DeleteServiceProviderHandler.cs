namespace _4EPlatform.Application.Features.ServiceProviderFeature.Command.Delete
{
    public class DeleteServiceProviderHandler : IRequestHandler<DeleteServiceProviderCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;

        public DeleteServiceProviderHandler(IUnitOfWork unitOfWork, IResponseHelper responseHelper)
        {
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
        }
        public async Task<ResponseDTO> Handle(DeleteServiceProviderCommand request, CancellationToken cancellationToken)
        {


            var ServiceProvider = _unitOfWork.Repository<ServiceProvider>().GetAllAsTracking(x => x.Id == request.Id && x.State == State.NotDeleted).FirstOrDefault();
            if (ServiceProvider == null)
                return _responseHelper.FailedToSave("ServiceProviderDoesNotExist");

            if (ServiceProvider.Offers.Where(o => o.State == State.NotDeleted).Count() > 0)
                return _responseHelper.FailedToSave("This service provider cannot be deleted because they have active offers");

            if (!string.IsNullOrEmpty(ServiceProvider.Image))
                Upload.DeleteFile(ServiceProvider.Image);
            if (ServiceProvider.ServiceProviderImages.Count() > 0)
            {
                foreach (var item in ServiceProvider.ServiceProviderImages)
                {
                    Upload.DeleteFile(item.ImagePath);
                    item.State = State.Deleted;

                }
                await _unitOfWork.Repository<ServiceProviderImage>().UpdateRangeAsync(ServiceProvider.ServiceProviderImages);



            }

            ServiceProvider.State = State.Deleted;

            await _unitOfWork.Repository<ServiceProvider>().UpdateAsync(ServiceProvider);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();


            return _responseHelper.SavedSuccessfully("ServiceProviderDeletedSuccessfully");
        }

    }
}
