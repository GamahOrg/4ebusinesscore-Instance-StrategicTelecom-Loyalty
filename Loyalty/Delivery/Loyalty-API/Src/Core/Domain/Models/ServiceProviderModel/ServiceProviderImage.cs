public class ServiceProviderImage : AuditEntity<Guid>
{
    public Guid ServiceProviderId { get; set; }
    public virtual ServiceProvider ServiceProvider { get; set; }
    public string ImagePath { get; set; }
    public string ImageName { get; set; }
}

