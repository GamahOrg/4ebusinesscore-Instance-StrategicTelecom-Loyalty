namespace _4EPlatform.Application.Features.ServiceProviderFeature.Query.GetServiceProviderByNameAr
{
    public class GetServiceProviderByNameArQuery : IRequest<ResponseDTO>
    {
        public string NameAr { get; set; }
        public GetServiceProviderByNameArQuery(string nameAr)
        {
            NameAr = nameAr;
        }

    }
}
