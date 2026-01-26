public class Lk_DiscountType : AuditEntity<Guid>
{
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public bool IsSeed { get; set; } = false;
    public virtual ICollection<Offer> Offers { get; set; } = new HashSet<Offer>();
}

