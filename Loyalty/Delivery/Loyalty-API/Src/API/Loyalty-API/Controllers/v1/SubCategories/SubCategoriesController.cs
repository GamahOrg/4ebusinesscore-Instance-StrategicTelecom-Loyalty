namespace _4EPlatform.API.Controllers.v1.SubCategories
{

    public class SubCategoriesController : BaseController
    {
        private readonly IMediator _mediator;

        public SubCategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetListSubCategories")]
        public async Task<ResponseDTO> GetListSubCategories([FromQuery] GetListSubCategoriesQuery command)
        {
            return await _mediator.Send(command);
        }
        [HttpPost("PostSubCategory")]
        public async Task<ResponseDTO> PostSubCategory([FromForm] PostSubCategoryDTO DTO)
        {
            return await _mediator.Send(new PostSubCategoryCommand { DTO = DTO });
        }
    }
}
