

namespace _4EPlatform.API.Controllers.v1.Indicators
{
    public class IndicatorController : BaseController


    {
        private readonly IMediator _mediator;
        public IndicatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseDTO> Get()

            => await _mediator.Send(new KPIsQuery());
    }
}
