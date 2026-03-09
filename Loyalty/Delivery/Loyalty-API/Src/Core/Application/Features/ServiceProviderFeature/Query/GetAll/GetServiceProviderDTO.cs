namespace _4EPlatform.Application.Features.ServiceProviderFeature.Query.GetAll
{
    public class GetServiceProviderDTO
    {
        public Guid Id { get; set; }
        public string TitelAr { get; set; }
        public string TitleEn { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public int OfferCounts { get; set; }

        public string Location { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Mobile { get; set; }

        public string? ServiceProviderImage { get; set; }

        public Status Status { get; set; }
        public DateTime CreateOn { get; set; }

    }
}
