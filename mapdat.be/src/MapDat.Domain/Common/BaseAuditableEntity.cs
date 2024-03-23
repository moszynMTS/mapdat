namespace MapDat.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModified { get; set; }
        public string LastModifiedBy { get; set; } = null!;
    }
}