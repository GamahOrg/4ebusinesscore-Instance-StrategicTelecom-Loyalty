namespace _4EPlatform.API.Controllers.v1.Lookup
{
    public class Lk_DiscountTypeController : BaseController
    {
        private readonly IMediator _mediator;
        public Lk_DiscountTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("PostDiscountType")]
        public async Task<ResponseDTO> PostDiscountType([FromForm] DiscountTypeDTO dto)
        {
            return await _mediator.Send(new PostDiscountTypeCommand(dto));
        }
        [HttpPut("PutDiscountType/{Id}")]
        public async Task<ResponseDTO> PutDiscountType(Guid Id, [FromForm] DiscountTypeDTO dto)
        {
            return await _mediator.Send(new PutDiscountTypeCommand(Id, dto));
        }
        [HttpDelete("DeleteDiscountType/{Id}")]
        public async Task<ResponseDTO> DeleteDiscountType(Guid Id)
        {
            return await _mediator.Send(new DeleteDiscountTypeCommand(Id));
        }
        [HttpGet("GetAllDiscountType")]
        public async Task<ResponseDTO> GetAllDiscountType()
        {
            return await _mediator.Send(new GetAllDiscountTypeQuery());
        }
        [HttpGet("GetByIdDiscountType/{Id}")]
        public async Task<ResponseDTO> GetByIdDiscountType(Guid Id)
        {
            return await _mediator.Send(new GetByIdDiscountTypeQuery(Id));
        }
    }
}
