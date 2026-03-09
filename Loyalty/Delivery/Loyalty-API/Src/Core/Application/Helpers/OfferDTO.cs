namespace _4EPlatform.Application.Features.Offers.Command
{
    public class OfferDTO
    {
        [PreventInjectionHTML]
        [PreventInjectionCSV]
        [PreventInjectionScript]
        public required string NameAr { get; set; }
        [PreventInjectionHTML]
        [PreventInjectionCSV]
        [PreventInjectionScript]
        public required string NameEN { get; set; }
        public required string DescriptionAr { get; set; }
        public required string DescriptionAEn { get; set; }
        public Guid? Lk_DiscountTypeId { get; set; }
        public Guid? Lk_DiscountMethodId { get; set; }
        public string? DiscountLink { get; set; }
        public string? DiscountAmount { get; set; }
        public string? Code { get; set; }
        public required string ConditionsAr { get; set; }
        public required string ConditionsEn { get; set; }
        public Guid SubCategoryId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime StartDate { get; set; }
        public required string StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public required string EndTime { get; set; }
        public IFormFile? ImagePth { get; set; }
        public Status Status { get; set; }
        public Guid ServiceProviderId { get; set; }
        public string? Other { get; set; }

    }
}
