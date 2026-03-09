namespace _4EPlatform.Application.Features.ServiceProviderFeature.Query.GetAll
{
    public class GetAllServiceProviderQuery : IRequest<ResponseDTO>
    {

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string? SearchWord { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }


    }
}
