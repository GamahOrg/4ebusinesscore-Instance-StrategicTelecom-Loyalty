namespace _4EPlatform.Application.Features.Offers.Query.GetAll
{
    public class GetOfferDTO
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEN { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionAEn { get; set; }

        public string ServiceProviderNameAr { get; set; }
        public string ServiceProviderNameEn { get; set; }
        public string? Lk_DiscountMethodAr { get; set; }
        public string? Lk_DiscountMethodEn { get; set; }
        public string? Lk_DiscountTypeAr { get; set; }
        public string? Lk_DiscountTypeEn { get; set; }


        public double? DiscountAmount { get; set; }
        public string? ConditionsAr { get; set; }
        public string? ConditionsEn { get; set; }
        public string? Code { get; set; }
        public string CategoryNameAr { get; set; }
        public string CategoryNameEn { get; set; }
        public string SubCategoryNameAr { get; set; }
        public string SubCategoryNameEn { get; set; }
        public string imagePath { get; set; }
        public string? imagePathServiceProvider { get; set; }
        public string? DiscountLink { get; set; }

        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }

        public Status Status { get; set; }
        public string? Other { get; set; }



    }
}
