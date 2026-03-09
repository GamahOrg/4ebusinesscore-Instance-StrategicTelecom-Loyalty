using _4EPlatform.Application.Features.ServiceProviderFeature.Command.Post;

namespace _4EPlatform.Application.Features.Command.Post
{
    public class PostServiceProviderCommand : IRequest<ResponseDTO>
    {
        public PostServiceProviderDTO DTO { get; set; }
    }
}
