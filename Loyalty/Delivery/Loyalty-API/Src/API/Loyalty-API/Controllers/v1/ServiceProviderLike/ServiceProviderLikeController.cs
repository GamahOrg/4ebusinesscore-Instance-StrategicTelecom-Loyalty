namespace _4EPlatform.API.Controllers.v1.ServiceProviderLike
{

    public class ServiceProviderLikeController : BaseController
    {
        private readonly IMediator _mediator;

        public ServiceProviderLikeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("PostServiceProviderLike")]
        public async Task<ResponseDTO> PostServiceProviderLike([FromForm] ServiceProviderLikeDTO DTO)
        {
            return await _mediator.Send(new ServiceProviderLikeCommand { DTO = DTO });
        }
    }
}
