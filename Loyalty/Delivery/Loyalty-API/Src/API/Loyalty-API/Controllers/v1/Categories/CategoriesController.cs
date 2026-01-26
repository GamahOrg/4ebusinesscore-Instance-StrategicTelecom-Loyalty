namespace _4EPlatform.API.Controllers.v1.Categories
{


    public class CategoriesController : BaseController
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("PostCategory")]
        public async Task<ResponseDTO> PostCategory([FromForm] PostCategoryPram CategoryPram)
            => await _mediator.Send(new PostCategoryCommand { PostCategoryPram = CategoryPram });


        [HttpPut("PutCategory")]
        public async Task<ResponseDTO> PutCategory([FromForm] PutCategoryPram CategoryPram)
            => await _mediator.Send(new PutCategoryCommand { PutCategoryPram = CategoryPram });

        [HttpGet("GetListCategory")]
        public async Task<ResponseDTO> GetListCategory([FromQuery] GetListCategoryQuery query)
            => await _mediator.Send(query);

        [HttpGet("GetAllCategories")]
        public async Task<ResponseDTO> GetAllCategories([FromQuery] GetAllCategoriesCommand command)
            => await _mediator.Send(command);

        [HttpGet("GetCategoryById/{CategoryId}")]
        public async Task<ResponseDTO> GetCategoryById(Guid CategoryId)

            => await _mediator.Send(new GetCategoryByIdCommand { Id = CategoryId });

        [HttpDelete("DeleteCategory/{CategoryId}")]
        public async Task<ResponseDTO> DeleteCategory(Guid CategoryId)
            => await _mediator.Send(new DeleteCategoryCommand { Id = CategoryId });

    }
}
