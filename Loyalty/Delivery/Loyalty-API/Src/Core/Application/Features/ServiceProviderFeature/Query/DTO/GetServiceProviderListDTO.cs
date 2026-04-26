namespace _4EPlatform.Application.Features.ServiceProviderFeature.Query.DTO
{
    public class GetServiceProviderListDTO
    {
        public Guid Id { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionAEn { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Mobile { get; set; }
        public string Number { get; set; }
        public string Image { get; set; }
        public bool Liked { get; set; }
        public Status Status { get; set; }
        public List<GetServiceProviderImageDTO> ServiceProviderImage { get; set; }
        public List<Offers.Query.DTO.GetOfferDTO> Offers { get; set; }
    }
    public class GetServiceProviderImageDTO
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
    }
}
