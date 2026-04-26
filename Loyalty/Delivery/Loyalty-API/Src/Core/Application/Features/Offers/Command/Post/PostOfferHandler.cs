

namespace _4EPlatform.Application.Features.Offers.Command.Post
{
    public class PostOfferHandler : IRequestHandler<PostOfferCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hosting;
        private readonly IResponseHelper _responseHelper;
        public readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public PostOfferHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hosting, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hosting = hosting;
            _responseHelper = responseHelper;

        }
        public async Task<ResponseDTO> Handle(PostOfferCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new PostOfferValidation(_unitOfWork, _stringLocalizer);
                var validation = validator.Validate(request);

                if (!validation.IsValid)
                    return _responseHelper.FailedToSave(string.Join("-", validation.Errors.Select(n => n.ErrorMessage).ToList()));

                var OfferMapped = _mapper.Map<Offer>(request.DTO);

                var imagePath = await Upload.UploadImage(request.DTO.ImagePth, _hosting, "offer/" + Guid.NewGuid() + "/Images");
                if (!imagePath.isSuccess)
                {
                    return _responseHelper.SavedSuccessfully(imagePath.Message);

                }

                OfferMapped.ImagePth = imagePath.Message;


                await _unitOfWork.Repository<Offer>().AddAsync(OfferMapped);



                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Commit();

                return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.AddSuccessfully] ?? "Error Localizer");

            }
            catch (Exception ex)
            {

                return _responseHelper.FailedToSave($"error: {ex.Message}");
            }
        }
    }
}


