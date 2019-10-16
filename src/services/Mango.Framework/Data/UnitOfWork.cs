using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
namespace Mango.Framework.Data
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext _context;
        private bool _disposed = false;
        private Dictionary<Type, object> _repositories;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public TContext DbContext => _context;
        public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : EntityBase
        {
            

            return null;
        }
        public int SaveChanges(bool ensureAutoHistory = false)
        {
           return _context.SaveChanges();
        }
        public Task<int> SaveChangesAsync(bool ensureAutoHistory = false)
        {
            return _context.SaveChangesAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // dispose the db context.
                    _context.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
