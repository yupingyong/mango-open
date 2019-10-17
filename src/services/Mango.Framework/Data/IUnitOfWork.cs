using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace Mango.Framework.Data
{

    /// <summary>
    /// 
    /// </summary>
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        TContext DbContext { get; }
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase;
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
