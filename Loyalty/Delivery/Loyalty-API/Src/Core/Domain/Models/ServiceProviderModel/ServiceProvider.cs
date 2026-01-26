using Domain.Utilities.Enums;

public class ServiceProvider : AuditEntity<Guid>
{
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public string DescriptionAr { get; set; }
    public string DescriptionAEn { get; set; }
    public string Location { get; set; }
    public string? Email { get; set; }
    public string Website { get; set; }
    public string? Mobile { get; set; }
    public string Image { get; set; }
    public Status Status { get; set; }
    public virtual ICollection<ServiceProviderImage> ServiceProviderImages { get; set; } = new HashSet<ServiceProviderImage>();
    public virtual ICollection<Offer> Offers { get; set; } = new HashSet<Offer>();
    public virtual ICollection<ServiceProviderLike> ServiceProviderLikes { get; set; } = new HashSet<ServiceProviderLike>();
}

