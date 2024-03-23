using MapDat.Domain.Authorization;
using MapDat.Domain.Common;
using MapDat.Domain.Entities;
using MapDat.Persistance.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace MapDat.Persistance.Context
{
    public class MapDatDbContext : DbContext, IMapDatDbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public MapDatDbContext(
            DbContextOptions<MapDatDbContext> options, 
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options) 
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        public DbSet<CargoEntity> Cargoes { get; set; }
      
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
