namespace MapDat.Domain.Common
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            IsActive = true;
        }
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}
