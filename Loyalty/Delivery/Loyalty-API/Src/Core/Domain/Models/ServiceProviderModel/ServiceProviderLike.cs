public class ServiceProviderLike : AuditEntity<Guid>
{
    public Guid ServiceProviderId { get; set; }
    public virtual ServiceProvider ServiceProvider { get; set; }
    public Guid UserId { get; set; }
}

