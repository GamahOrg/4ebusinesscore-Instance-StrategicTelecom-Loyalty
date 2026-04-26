public class OfferLikeUsed : AuditEntity<Guid>
{
    public Guid OfferId { get; set; }
    public virtual Offer Offer { get; set; }
    public Guid UserId { get; set; }
    public OfferType OfferType { get; set; }

}


