namespace _4EPlatform.Application.Helpers
{
    public interface ITokenService
    {
        Task<CallOptions> AcquireToken();


    }
}
