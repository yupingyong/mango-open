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
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Query();

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entity);

        IDbContextTransaction BeginTransaction();

        void SaveChanges();

        Task SaveChangesAsync();

        void Remove(TEntity entity);
    }
}
