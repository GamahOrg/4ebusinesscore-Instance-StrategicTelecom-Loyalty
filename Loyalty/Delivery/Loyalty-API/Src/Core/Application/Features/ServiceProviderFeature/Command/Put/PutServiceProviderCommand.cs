namespace _4EPlatform.Application.Features.Command.Put
{
    public class PutServiceProviderCommand : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
        public PutServiceProviderDTO DTO { get; set; }

    }
}
