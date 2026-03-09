namespace _4EPlatform.Application.Features.Offers.Query.DTO
{
    public class GetOfferDTO
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEN { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionAEn { get; set; }
        public string DiscountTypeName { get; set; }
        public string DiscountMethodName { get; set; }
        public Guid? Lk_DiscountTypeId { get; set; }
        public Guid? Lk_DiscountMethodId { get; set; }
        public string DiscountLink { get; set; }
        public string DiscountAmount { get; set; }
        public string? ConditionsAr { get; set; }
        public string? ConditionsEn { get; set; }
        public string SubCategoryName { get; set; }
        public string serviceProviderLocation { get; set; }
        public string serviceProviderwebSite { get; set; }
        public Guid ServiceProviderId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }
        public string? Code { get; set; }
        public string CategoryName { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? ImagePth { get; set; }
        public Status Status { get; set; }
        public string ServiceProviderNameAr { get; set; }
        public string ServiceProviderNameEn { get; set; }
        public string ServiceProviderImage { get; set; }
        public string? Other { get; set; }

        public bool Liked { get; set; }
    }
}
