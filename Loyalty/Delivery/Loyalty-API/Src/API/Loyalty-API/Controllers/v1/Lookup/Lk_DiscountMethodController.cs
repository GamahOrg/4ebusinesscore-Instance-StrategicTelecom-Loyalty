namespace _4EPlatform.API.Controllers.v1.Lookup
{
    public class Lk_DiscountMethodController : BaseController
    {
        private readonly IMediator _mediator;
        public Lk_DiscountMethodController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("PostDiscountMethod")]
        public async Task<ResponseDTO> PostDiscountMethod([FromForm] DiscountMethodDTO dto)

            => await _mediator.Send(new PostDiscountMethodCommand(dto));

        [HttpPut("PutDiscountMethod/{Id}")]
        public async Task<ResponseDTO> PutDiscountMethod(Guid Id, [FromForm] DiscountMethodDTO dto)

            => await _mediator.Send(new PutDiscountMethodCommand(Id, dto));

        [HttpDelete("DeleteDiscountMethod/{Id}")]
        public async Task<ResponseDTO> DeleteDiscountMethod(Guid Id)

            => await _mediator.Send(new DeleteDiscountMethodCommand(Id));

        [HttpGet("GetAllDiscountMethod")]
        public async Task<ResponseDTO> GetAllDiscountMethod()

            => await _mediator.Send(new GetAllDiscountMethodQuery());

        [HttpGet("GetByIdDiscountMethod/{Id}")]
        public async Task<ResponseDTO> GetByIdDiscountMethod(Guid Id)

            => await _mediator.Send(new GetByIdDiscountMethodQuery(Id));

    }
}
