using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Data.SqlClient;
using Microsoft.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Mango.Framework.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public Repository(MangoDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        protected DbContext Context { get; }

        protected DbSet<TEntity> DbSet { get; }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entity)
        {
            DbSet.AddRange(entity);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }
        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public IQueryable<TEntity> Query()
        {
            return DbSet;
        }

        
    }
}
