using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;

namespace SvarnyJunak.CeskeObce.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> FindAll(IQuery<T> query);
        bool Exists(IQuery<T> query);
        Task ReplaceAllAsync(T[] municipalities);
    }

    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected IQueryable<T> Data = new T[0].AsQueryable();
        private readonly IDataStorage<T> dataStorage;

        protected RepositoryBase(IDataStorage<T> dataStorage)
        {
            this.dataStorage = dataStorage;
            this.Data = dataStorage.Load().AsQueryable();
        }

        public IEnumerable<T> FindAll()
        {
            return Data;
        }

        public IEnumerable<T> FindAll(IQuery<T> query)
        {
            return Data.Where(query.Expression);
        }

        public bool Exists(IQuery<T> query)
        {
            return Data.Any(query.Expression);
        }

        public async Task ReplaceAllAsync(T[] data)
        {
            await dataStorage.StoreAsync(data);
        }
    }
}
