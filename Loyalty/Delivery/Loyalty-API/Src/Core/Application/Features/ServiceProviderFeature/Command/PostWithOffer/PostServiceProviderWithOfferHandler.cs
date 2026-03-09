

namespace _4EPlatform.Application.Features.ServiceProviderFeature.Command.PostWithOffer
{
    public class PostServiceProviderWithOfferHandler : IRequestHandler<PostServiceProviderWitOfferCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hosting;
        private readonly IResponseHelper _responseHelper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public PostServiceProviderWithOfferHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hosting, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hosting = hosting;
            _responseHelper = responseHelper;

        }
        public async Task<ResponseDTO> Handle(PostServiceProviderWitOfferCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new PostServiceProviderWithOfferValidation(_unitOfWork, _stringLocalizer);
                var validation = validator.Validate(request);

                if (!validation.IsValid)
                    return _responseHelper.FailedToSave(string.Join("-", validation.Errors.Select(n => n.ErrorMessage).ToList()));


                var ServiceProviderMapped = _mapper.Map<ServiceProvider>(request.DTO);

                var image = await Upload.UploadImage(request.DTO.Image, _hosting, "LogoServiceProvider/" + Guid.NewGuid() + "/Images");
                if (!image.isSuccess)
                {
                    return _responseHelper.SavedSuccessfully(image.Message);

                }


                ServiceProviderMapped.Image = image.Message;




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





                #region offer
                var offermapped = _mapper.Map<Offer>(request.DTO.offerDTO);

                var imageOffer = await Upload.UploadImage(request.DTO.offerDTO.ImagePth, _hosting, "LogoServiceProvider/" + request.DTO.NameEn + "/Images");
                if (!imageOffer.isSuccess)
                {
                    return _responseHelper.SavedSuccessfully(image.Message);

                }


                offermapped.ImagePth = imageOffer.Message;
                offermapped.ServiceProviderId = ServiceProviderMapped.Id;




                ServiceProviderMapped.Offers.Add(offermapped);
                #endregion

                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Commit();

                return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.AddSuccessfully] ?? "Error Localizer");

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
