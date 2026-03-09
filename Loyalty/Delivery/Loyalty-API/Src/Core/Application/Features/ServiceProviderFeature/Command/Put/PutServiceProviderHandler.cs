namespace _4EPlatform.Application.Features.Command.Put
{
    public class PutServiceProviderHandler : IRequestHandler<PutServiceProviderCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hosting;
        private readonly IResponseHelper _responseHelper;
        public readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public PutServiceProviderHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hosting, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hosting = hosting;
            _responseHelper = responseHelper;

        }
        public async Task<ResponseDTO> Handle(PutServiceProviderCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var validator = new PutServiceProviderValidation(_unitOfWork, _stringLocalizer);
                var validation = validator.Validate(request);

                if (!validation.IsValid)
                    return _responseHelper.FailedToSave(string.Join("-", validation.Errors.Select(n => n.ErrorMessage).ToList()));




                var oldServiceProvider = _unitOfWork.Repository<ServiceProvider>().GetAllAsTracking(x => x.Id == request.Id).FirstOrDefault();
                if (oldServiceProvider == null)
                {
                    return _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);
                }


                string imagePath = oldServiceProvider.Image;

                if (request.DTO.Image != null)
                {
                    Upload.DeleteFile(oldServiceProvider.Image);

                    var imagePathCheck = await Upload.UploadImage(request.DTO.Image, _hosting, "LogoServiceProvider/" + Guid.NewGuid() + "/Images");
                    if (!imagePathCheck.isSuccess)
                    {
                        return _responseHelper.SavedSuccessfully(imagePathCheck.Message);

                    }

                    imagePath = imagePathCheck.Message;

                }


                var ServiceProviderMapped = _mapper.Map(request.DTO, oldServiceProvider);
                ServiceProviderMapped.Image = imagePath;


                if (request.DTO?.Ids?.Count() > 0)
                {
                    foreach (var item in request.DTO?.Ids)
                    {
                        var old = _unitOfWork.Repository<ServiceProviderImage>().GetAllAsTracking(x => x.State != State.Deleted).FirstOrDefault(x => x.Id == item);




                        Upload.DeleteFile(old?.ImagePath);
                        old.State = State.Deleted;
                        await _unitOfWork.Repository<ServiceProviderImage>().UpdateAsync(old);


                    }

                }


                if (request?.DTO?.ServiceProviderImages.Count() > 0)
                {




                    var images = new List<ServiceProviderImage>();

                    foreach (var item in request.DTO.ServiceProviderImages)
                    {
                        var file = await Upload.UploadFiles(item, _hosting, "ImageServiceProvider/" + Guid.NewGuid() + "Attachments");
                        var attachment = new ServiceProviderImage()
                        {
                            ImageName = item.FileName,
                            ImagePath = file,
                            ServiceProviderId = ServiceProviderMapped.Id,

                        };

                        images.Add(attachment);
                    }

                    ServiceProviderMapped.ServiceProviderImages = images;
                }

                await _unitOfWork.Repository<ServiceProvider>().UpdateAsync(ServiceProviderMapped);


                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Commit();

                return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.UpdateSuccessfully]);

            }
            catch (Exception ex)
            {
                return _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.FailedToSave]);
            }


        }
    }

}

