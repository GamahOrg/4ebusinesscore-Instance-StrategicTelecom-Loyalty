namespace _4EPlatform.Application.Features.Offers.Query.GetAllWithFilter
{
    public class GetAllOfferWithFilterDto
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEN { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionAEn { get; set; }
        public double? DiscountAmount { get; set; }
        public string ImagePth { get; set; }
        public string ServiceProviderNameAr { get; set; }
        public string ServiceProviderNameEn { get; set; }

        public string ServiceProviderImage { get; set; }
        public DateTime EndDate { get; set; }
    }
}
