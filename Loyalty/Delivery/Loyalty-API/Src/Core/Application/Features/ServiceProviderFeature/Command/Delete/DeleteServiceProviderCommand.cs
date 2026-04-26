namespace _4EPlatform.Application.Features.ServiceProviderFeature.Command.Delete
{
    public class DeleteServiceProviderCommand : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
    }
}
