using Domain.Utilities.Enums;

public class SubCategory : AuditEntity<Guid>
{
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public Status Status { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<Offer> Offers { get; set; } = new HashSet<Offer>();
}

