using System;

namespace KP.Storage.Domain.Context
{
    public interface IDbContextFactory : IDisposable
    {
        IDbContext GetContext();
    }
}