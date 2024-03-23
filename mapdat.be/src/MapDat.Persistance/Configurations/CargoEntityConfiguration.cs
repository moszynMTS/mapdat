using MapDat.Domain.Entities;
using MapDat.Persistance.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MapDat.Persistance.Configurations
{
    public class CargoEntityConfiguration : BaseEntityConfiguration<CargoEntity>
    {
        private readonly static string _tableName = "Cargoes";

        public CargoEntityConfiguration() : base(_tableName) { }

        public override void Configure(EntityTypeBuilder<CargoEntity> builder)
        {
            base.Configure(builder);
        }
    }
}
