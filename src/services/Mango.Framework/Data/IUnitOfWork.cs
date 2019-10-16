using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace Mango.Framework.Data
{

    /// <summary>
    /// Defines the interface(s) for generic unit of work.
    /// </summary>
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        TContext DbContext { get; }
        IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : EntityBase;
        int SaveChanges(bool ensureAutoHistory = false);
        Task<int> SaveChangesAsync(bool ensureAutoHistory = false);
    }
}
