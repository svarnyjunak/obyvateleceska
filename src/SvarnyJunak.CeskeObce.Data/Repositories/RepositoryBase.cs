using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;

namespace SvarnyJunak.CeskeObce.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<Municipality> FindAll();
        IEnumerable<T> FindAll(IQuery<T> query);
        bool Exists(IQuery<T> query);
        void Save(T[] municipalities);
    }

    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly CeskeObceDbContext DbContext;

        protected RepositoryBase(CeskeObceDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IEnumerable<Municipality> FindAll()
        {
            return DbContext.Municipalities;
        }

        public IEnumerable<T> FindAll(IQuery<T> query)
        {
            return GetDbSet().Where(query.Expression.Compile());
        }

        public bool Exists(IQuery<T> query)
        {
            return GetDbSet().Any(query.Expression.Compile());
        }

        public void Save(T[] data)
        {
            DbContext.RemoveRange(GetDbSet());
            DbContext.AddRange(data);
            DbContext.SaveChanges();
        }

        protected abstract DbSet<T> GetDbSet();
    }
}
