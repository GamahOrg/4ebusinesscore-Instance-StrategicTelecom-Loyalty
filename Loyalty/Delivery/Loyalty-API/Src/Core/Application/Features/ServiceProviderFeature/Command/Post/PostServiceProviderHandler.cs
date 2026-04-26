namespace _4EPlatform.Application.Features.Command.Post
{
    public class PostServiceProviderHandler : IRequestHandler<PostServiceProviderCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hosting;
        private readonly IResponseHelper _responseHelper;
        public readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public PostServiceProviderHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hosting, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hosting = hosting;
            _responseHelper = responseHelper;

        }
        public async Task<ResponseDTO> Handle(PostServiceProviderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new PostServiceProviderValidation(_stringLocalizer, _unitOfWork);
                var validation = validator.Validate(request);

                if (!validation.IsValid)
                    return _responseHelper.FailedToSave(string.Join("-", validation.Errors.Select(n => n.ErrorMessage).ToList()));


                var ServiceProviderMapped = _mapper.Map<ServiceProvider>(request.DTO);

                var imagePath = await Upload.UploadImage(request.DTO.Image, _hosting, "LogoServiceProvider/" + Guid.NewGuid() + "/Images");

                if (!imagePath.isSuccess)
                {
                    return _responseHelper.SavedSuccessfully(imagePath.Message);

                }

                ServiceProviderMapped.Image = imagePath.Message;

                await _unitOfWork.Repository<ServiceProvider>().AddAsync(ServiceProviderMapped);
                if (request.DTO.ServiceProviderImages.Count() > 0)
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

                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Commit();

                return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.AddSuccessfully] ?? "Error Localizer");

            }
            catch (Exception ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;
                return _responseHelper.FailedToSave(message);

            }
        }
    }
}
