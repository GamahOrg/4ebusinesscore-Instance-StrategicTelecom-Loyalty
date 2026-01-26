namespace _4EPlatform.API.Controllers.v1.OfferLikeUse
{

    public class OfferLikeUsedController : BaseController
    {
        private readonly IMediator _mediator;

        public OfferLikeUsedController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("PostOfferLikeUsed")]
        public async Task<ResponseDTO> PostOfferLikeUsed([FromForm] OfferLikeUsedCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
