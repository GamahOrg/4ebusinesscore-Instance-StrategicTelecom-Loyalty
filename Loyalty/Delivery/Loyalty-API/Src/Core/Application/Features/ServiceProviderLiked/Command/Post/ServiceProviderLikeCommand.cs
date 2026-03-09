namespace _4EPlatform.Application.Features.ServiceProviderLiked.Command.Post
{
    public class ServiceProviderLikeCommand : IRequest<ResponseDTO>
    {
        public ServiceProviderLikeDTO DTO { get; set; } = new ServiceProviderLikeDTO();
    }
}
