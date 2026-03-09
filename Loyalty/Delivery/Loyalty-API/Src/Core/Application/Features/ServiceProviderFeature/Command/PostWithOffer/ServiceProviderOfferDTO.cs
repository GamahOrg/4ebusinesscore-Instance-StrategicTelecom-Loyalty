namespace _4EPlatform.Application.Features.ServiceProviderFeature.Command.PostWithOffer
{
    public class ServiceProviderOfferDTO
    {
        [PreventInjectionHTML]
        [PreventInjectionCSV]
        [PreventInjectionScript]
        public string NameAr { get; set; }
        [PreventInjectionHTML]
        [PreventInjectionCSV]
        [PreventInjectionScript]
        public string NameEN { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionAEn { get; set; }
        public Guid? Lk_DiscountTypeId { get; set; }
        public Guid? Lk_DiscountMethodId { get; set; }
        public string? DiscountLink { get; set; }
        public string? DiscountAmount { get; set; }
        public string? Code { get; set; }
        public string? ConditionsAr { get; set; }
        public string? ConditionsEn { get; set; }
        public Guid SubCategoryId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime StartDate { get; set; }
        public string StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public string EndTime { get; set; }
        public IFormFile ImagePth { get; set; }

        public string? Other { get; set; }
    }
}
