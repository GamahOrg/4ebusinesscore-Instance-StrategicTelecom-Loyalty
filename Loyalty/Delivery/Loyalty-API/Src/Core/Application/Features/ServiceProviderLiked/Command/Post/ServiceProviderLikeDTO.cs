namespace _4EPlatform.Application.Features.ServiceProviderLiked.Command.Post
{
    public class ServiceProviderLikeDTO
    {
        public Guid ServiceProviderId { get; set; }
        public bool IsLike { get; set; }
    }
}
