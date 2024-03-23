using MapDat.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MapDat.Persistance.Common
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> 
        where TEntity : BaseAuditableEntity
    {
        private readonly string _tableName;

        public BaseEntityConfiguration(string tableName)
        {
            _tableName = tableName;
        }

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(_tableName);

            builder.HasKey(e => e.Id);
        }
    }
}
