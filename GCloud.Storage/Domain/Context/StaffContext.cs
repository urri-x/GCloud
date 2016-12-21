using System.Data.Entity;
using KP.Storage.Repository;

namespace KP.Storage.Domain.Context
{
    [DbConfigurationType(typeof(NpgsqlConfiguration))]
    public class StaffContext : DbContext, IDbContext
    {
        public StaffContext(string nameOrConnectionString) : base(nameOrConnectionString) { }
        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // ToDo: Use Configuration from assembly
            // PostgreSQL uses the public schema by default - not dbo.
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StaffObject>()
                .Map(m =>
                {
                    m.Requires("StaffObjectType");
                });
        }
    }

    public class NpgsqlConfiguration: System.Data.Entity.DbConfiguration
    {
        public NpgsqlConfiguration()
        {
            SetProviderServices("Npgsql", Npgsql.NpgsqlServices.Instance);
            SetProviderFactory("Npgsql", Npgsql.NpgsqlFactory.Instance);
            SetDefaultConnectionFactory(new Npgsql.NpgsqlConnectionFactory());
        }
    }
}