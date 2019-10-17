using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
namespace Mango.Framework.Data
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : MangoDbContext
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
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }
            var customRepo = _context.GetService<IRepository<TEntity>>();
            if (customRepo != null)
            {
                return customRepo;
            }
            return new Repository<TEntity>(_context);
        }
        public int SaveChanges()
        {
           return _context.SaveChanges();
        }
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
