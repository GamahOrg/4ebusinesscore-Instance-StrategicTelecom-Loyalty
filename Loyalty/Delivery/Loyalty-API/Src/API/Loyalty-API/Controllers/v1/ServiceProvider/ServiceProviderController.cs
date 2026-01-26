

using _4EPlatform.Application.Features.ServiceProviderFeature.Query.GetServiceProviderByNameAr;
using _4EPlatform.Application.Features.ServiceProviderFeature.Query.GetServiceProviderByNameEn;

namespace _4EPlatform.API.Controllers.v1
{

    public class ServiceProviderController : BaseController
    {
        private readonly IMediator _mediator;

        public ServiceProviderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region Post 
        [HttpPost("PostServiceProvider")]
        public async Task<ResponseDTO> PostServiceProvider([FromForm] PostServiceProviderDTO DTO)
        {
            return await _mediator.Send(new PostServiceProviderCommand { DTO = DTO });
        }
        #endregion

        #region PostWithOffer 
        [HttpPost("PostServiceProviderWithOffer")]
        public async Task<ResponseDTO> PostServiceProviderWithOffer([FromForm] ServiceProviderWithOfferDTO DTO)
        {
            return await _mediator.Send(new PostServiceProviderWitOfferCommand { DTO = DTO });
        }
        #endregion


        #region Put 
        [HttpPut("PutServiceProvider/{Id}")]
        public async Task<ResponseDTO> PutServiceProvider(Guid Id, [FromForm] PutServiceProviderDTO DTO)
        {
            return await _mediator.Send(new PutServiceProviderCommand { DTO = DTO, Id = Id });
        }
        #endregion


        #region Get 

        [HttpGet("GetListServiceProvider")]
        public async Task<ResponseDTO> GetListServiceProvider([FromQuery] GetListServiceProviderQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet("GetServiceProviderById/{Id}")]
        public async Task<ResponseDTO> GetServiceProviderById(Guid Id)
        {
            return await _mediator.Send(new GetServiceProviderByIdQuery(Id));
        }
        [HttpGet("GetServiceProviderByNameAr/{NameAr}")]
        public async Task<ResponseDTO> GetServiceProviderByNameAr(string NameAr)
        {
            return await _mediator.Send(new GetServiceProviderByNameArQuery(NameAr));
        }

        [HttpGet("GetServiceProviderByNameEn/{NameEn}")]
        public async Task<ResponseDTO> GetServiceProviderByNameEn(string NameEn)
        {
            return await _mediator.Send(new GetServiceProviderByNameEnQuery(NameEn));
        }

        [HttpGet("GetAllServiceProvider")]
        public async Task<ResponseDTO> GetAllServiceProvider(int PageNumber, int PageSize, string? SearchWord, Guid? CategoryId, Guid? SubCategoryId)
        {
            return await _mediator.Send(new GetAllServiceProviderQuery
            { PageNumber = PageNumber, PageSize = PageSize, SearchWord = SearchWord, CategoryId = CategoryId, SubCategoryId = SubCategoryId });
        }
        [HttpGet("GetAllServiceProviderWithFilter")]
        public async Task<ResponseDTO> GetAllServiceProviderWithFilter([FromQuery] GetAllServiceProviderCommand query)
        {
            return await _mediator.Send(query);
        }


        #endregion

        #region Delete 
        [HttpDelete("DeleteServiceProvider/{Id}")]
        public async Task<ResponseDTO> DeleteServiceProvider(Guid Id)
        {
            return await _mediator.Send(new DeleteServiceProviderCommand { Id = Id });
        }
        #endregion
    }
}
