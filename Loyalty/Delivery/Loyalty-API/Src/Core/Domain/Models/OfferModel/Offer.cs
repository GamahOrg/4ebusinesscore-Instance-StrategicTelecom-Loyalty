public class Offer : AuditEntity<Guid>
{
    public string NameAr { get; set; }
    public string NameEN { get; set; }
    public string DescriptionAr { get; set; }
    public string DescriptionAEn { get; set; }
    public Guid? Lk_DiscountTypeId { get; set; }
    public virtual Lk_DiscountType? Lk_DiscountType { get; set; }
    public Guid? Lk_DiscountMethodId { get; set; }
    public virtual Lk_DiscountMethod? Lk_DiscountMethod { get; set; }
    public string? DiscountLink { get; set; }
    public double? DiscountAmount { get; set; }

    public Guid SubCategoryId { get; set; }
    public virtual SubCategory SubCategory { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public DateTime StartDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan EndTime { get; set; }
    public string ImagePth { get; set; }
    public Status Status { get; set; }
    public string? Code { get; set; }
    public string ConditionsAr { get; set; }
    public string ConditionsEn { get; set; }
    public string? Other { get; set; }



    public Guid ServiceProviderId { get; set; }
    public virtual ServiceProvider ServiceProvider { get; set; }
    public virtual ICollection<OfferLikeUsed> OfferLikeUseds { get; set; } = new HashSet<OfferLikeUsed>();
}

