namespace _4EPlatform.Application.Features.ServiceProviderFeature.Query.GetAllServiceProvider
{
    public class GetAllServiceProviderDto
    {
        public Guid Id { get; set; }
        public string TitelAr { get; set; }
        public string TitleEn { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Image { get; set; }
        public int offerCount { get; set; }
    }
}
