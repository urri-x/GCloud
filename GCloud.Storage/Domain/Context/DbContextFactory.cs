using System.Data.Entity.Core.EntityClient;

namespace KP.Storage.Domain.Context
{
    public class DbContextFactory : IDbContextFactory
    {
        private string connectionString => "Database=KP;Password=admin000;Username=postgres;Host=localhost";
        private StaffContext context;
        public IDbContext GetContext()
        {
            return context ?? (context = new StaffContext(connectionString));
        }

        

        public void Dispose()
        {
            context.Dispose();
        }
    }
}