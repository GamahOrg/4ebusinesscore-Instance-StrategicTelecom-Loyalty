namespace _4EPlatform.Application.Features.ServiceProviderLiked.Command.Post
{
    public class ServiceProviderLikeCommandHandler : IRequestHandler<ServiceProviderLikeCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        public Guid _loggedInUserId;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public ServiceProviderLikeCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IResponseHelper responseHelper, IHttpContextAccessor httpContextAccessor)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
            _loggedInUserId = LoggedInUserProvider.GetLoggedInUserId(httpContextAccessor);
        }
        public async Task<ResponseDTO> Handle(ServiceProviderLikeCommand request, CancellationToken cancellationToken)
        {
            var serviceProvider = _unitOfWork.Repository<ServiceProvider>().GetAllAsNoTracking(x => x.Id == request.DTO.ServiceProviderId).FirstOrDefault();

            if (serviceProvider == null)
            {
                return _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.NotFound]);
            }
            var like = _unitOfWork.Repository<ServiceProviderLike>().GetAllAsNoTracking(x => x.ServiceProviderId == request.DTO.ServiceProviderId && x.UserId == _loggedInUserId).FirstOrDefault();
            if (like == null && request.DTO.IsLike == true)
            {
                var newLike = new ServiceProviderLike
                {
                    ServiceProviderId = request.DTO.ServiceProviderId,
                    UserId = _loggedInUserId,
                };

                await _unitOfWork.Repository<ServiceProviderLike>().AddAsync(newLike);
            }
            else if (like != null && request.DTO.IsLike == false)
            {
                like.State = State.Deleted;
                await _unitOfWork.Repository<ServiceProviderLike>().UpdateAsync(like);
            }
            else
            {
                return _responseHelper.FailedToSave("sorry, UserLogin not like ServiceProvider Before to DisLike It ");
            }

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.AddSuccessfully]);
        }
    }
}
