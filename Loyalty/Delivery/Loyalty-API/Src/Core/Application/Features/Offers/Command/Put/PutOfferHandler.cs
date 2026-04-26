namespace _4EPlatform.Application.Features.Offers.Command.Put
{
    public class PutOfferHandler : IRequestHandler<PutOfferCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hosting;
        private readonly IResponseHelper _responseHelper;
        public readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public PutOfferHandler(IStringLocalizer<SharedResource> stringLocalizer, IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hosting, IResponseHelper responseHelper)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hosting = hosting;
            _responseHelper = responseHelper;

        }
        public async Task<ResponseDTO> Handle(PutOfferCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var validator = new PutOfferValidation(_unitOfWork, _stringLocalizer);
                var validation = validator.Validate(request);

                if (!validation.IsValid)
                    return _responseHelper.FailedToSave(string.Join("-", validation.Errors.Select(n => n.ErrorMessage).ToList()));

                var OldOfferProvider = _unitOfWork.Repository<Offer>().GetAllAsTracking(x => x.Id == request.Id).FirstOrDefault();
                if (OldOfferProvider == null)
                {
                    return _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound]);
                }


                if (request.DTO.ImagePth != null)
                {
                    Upload.DeleteFile(OldOfferProvider.ImagePth);

                    var imagePath = await Upload.UploadImage(request.DTO.ImagePth, _hosting, "Offer/" + Guid.NewGuid() + "/Images");
                    if (!imagePath.isSuccess)
                    {
                        return _responseHelper.SavedSuccessfully(imagePath.Message);

                    }

                    OldOfferProvider.ImagePth = imagePath.Message;
                }

                var OfferMapped = _mapper.Map(request.DTO, OldOfferProvider);




                await _unitOfWork.Repository<Offer>().UpdateAsync(OfferMapped);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Commit();

                return _responseHelper.SavedSuccessfully(_stringLocalizer[SharedResourceKeys.UpdateSuccessfully]);
            }
            catch (Exception ex)
            {

                return _responseHelper.FailedToSave($"error: {ex.Message}");
            }

        }

    }

}

