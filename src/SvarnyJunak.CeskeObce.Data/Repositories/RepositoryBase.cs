using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        Task ReplaceAllAsync(T[] municipalities);
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
            return GetDbSet().Where(query.Expression);
        }

        public bool Exists(IQuery<T> query)
        {
            return GetDbSet().Any(query.Expression);
        }

        public async Task ReplaceAllAsync(T[] data)
        {
            DbContext.RemoveRange(GetDbSet());
            await DbContext.AddRangeAsync(data);
            await DbContext.SaveChangesAsync();
        }

        protected abstract DbSet<T> GetDbSet();
    }
}
