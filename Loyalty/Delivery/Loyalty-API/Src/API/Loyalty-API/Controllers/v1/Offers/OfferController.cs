

using Domain.Utilities.Enums;

namespace _4EPlatform.API.Controllers.v1.Offers
{
    public class OfferController : BaseController
    {
        private readonly IMediator _mediator;

        public OfferController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region Post 
        [HttpPost("PostOffer")]
        public async Task<ResponseDTO> PostOffer([FromForm] PostOfferDTO DTO)
        {
            return await _mediator.Send(new PostOfferCommand { DTO = DTO });
        }
        #endregion

        #region Put 
        [HttpPut("PutOffer/{Id}")]
        public async Task<ResponseDTO> PutOffer(Guid Id, [FromForm] PutOfferDTO DTO)
        {
            return await _mediator.Send(new PutOfferCommand { DTO = DTO, Id = Id });
        }
        #endregion

        #region GetById 

        [HttpGet("GetOfferById/{Id}")]
        public async Task<ResponseDTO> GetOfferById(Guid Id)
        {
            return await _mediator.Send(new GetOfferByIdQuery(Id));
        }


        [HttpGet("GetOffersByServiceProviderId/{Id}")]
        public async Task<ResponseDTO> GetOffersByServiceProviderId(Guid Id)
        {
            return await _mediator.Send(new GetOffersByIServiceProviderQuery(Id));
        }
        #endregion



        #region GetAll 

        [HttpGet("GetAllOffer")]
        public async Task<ResponseDTO> GetAllOffer(
        #region filter

                                             int PageNumber, int PageSize, string? SearchWord,

                                                  TimeSpan? EndTime,
                                                  DateTime? EndDate,
                                                  TimeSpan? StartTime,
                                                   DateTime? StartDate,
                                                   Status status,
                                                   Guid CategoryId,
                                                   Guid SubCategoryId
        #endregion

                                                 )
        {
            return await _mediator.Send(new GetAllOfferQuery
            {
                CategoryId = CategoryId,
                SubCategoryId = SubCategoryId,
                PageNumber = PageNumber,
                PageSize = PageSize,
                StartDate = StartDate,
                StartTime = StartTime,
                EndDate = EndDate,
                EndTime = EndTime,
                SearchWord = SearchWord,
                status = status,
            });
        }
        [HttpGet("GetAllOffersWithFilter")]
        public async Task<ResponseDTO> GetAllOffersWithFilter([FromQuery] GetAllOfferWithFilterCommand query)
        {
            return await _mediator.Send(query);
        }
        #endregion

        #region Delete 
        [HttpDelete("DeleteOffer/{Id}")]
        public async Task<ResponseDTO> DeleteOffer(Guid Id)
        {
            return await _mediator.Send(new DeleteOfferCommand { Id = Id });
        }
        #endregion

    }
}

