namespace _4EPlatform.Application.Features.ServiceProviderFeature.Query.GetServiceProviderById
{
    public class GetServiceProviderByIdQuery : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
        public GetServiceProviderByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

