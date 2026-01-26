//namespace _4EPlatform.API.Controllers.v1.Lookup
//{
//public class LK_OfferTypeController : BaseController
//{
//    private readonly IMediator _mediator;
//    public LK_OfferTypeController(IMediator mediator)
//    {
//        _mediator = mediator;
//    }
//    [HttpPost("PostLK_OfferTypeMethod")]
//    public async Task<ResponseDTO> PostLK_OfferTypeMethod([FromForm] DiscountTypeDTO dto)

//        => await _mediator.Send(new PostLK_OfferTypeCommand(dto));

//    [HttpPut("PutLK_OfferType/{Id}")]
//    public async Task<ResponseDTO> PutLK_OfferType(Guid Id, [FromForm] LK_OfferTypeDTO dto)

//        => await _mediator.Send(new PutLk_OfferTypeCommand(Id, dto));


//    [HttpDelete("DeleteOfferType/{Id}")]
//    public async Task<ResponseDTO> DeleteOfferType(Guid Id)

//        => await _mediator.Send(new DeleteLK_OfferTypeCommand(Id));

//    [HttpGet("GetAllK_OfferType")]
//    public async Task<ResponseDTO> GetAllLK_OfferType()

//         => await _mediator.Send(new GetAllDiscountTypeQuery());

//    [HttpGet("GetByIdlK_OfferType/{Id}")]
//    public async Task<ResponseDTO> GetByIdlK_OfferType(Guid Id)

//        => await _mediator.Send(new GetByIdDiscountTypeQuery(Id));


//}
//}

