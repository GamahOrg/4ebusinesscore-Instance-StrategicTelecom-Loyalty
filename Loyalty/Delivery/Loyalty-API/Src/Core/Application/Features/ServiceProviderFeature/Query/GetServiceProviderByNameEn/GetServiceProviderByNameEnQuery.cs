namespace _4EPlatform.Application.Features.ServiceProviderFeature.Query.GetServiceProviderByNameEn
{
    public class GetServiceProviderByNameEnQuery : IRequest<ResponseDTO>
    {
        public string NameEn { get; set; }
        public GetServiceProviderByNameEnQuery(string nameEn)
        {
            NameEn = nameEn;
        }

    }
}
