
public class Category : AuditEntity<Guid>
{
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public string DescriptionAr { get; set; }
    public string DescriptionAEn { get; set; }
    public string Iconpath { get; set; }

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new HashSet<SubCategory>();
    public virtual ICollection<Offer> Offers { get; set; } = new HashSet<Offer>();
}

