using SvarnyJunak.CeskeObce.Data.Repositories;
using System.Threading.Tasks;

namespace SvarnyJunak.CeskeObce.Data.Test.Repositories
{
    public class MemoryDataStorage<T> : IDataStorage<T>
    {
        private T[] data;

        public MemoryDataStorage(params T[] data)
        {
            this.data = data;
        }

        public T[] Load()
        {
            return data;
        }

        public Task StoreAsync(T[] data)
        {
            this.data = data;

            return Task.CompletedTask;
        }
    }

    public static class MemoryDataStorage
    {
        public static MemoryDataStorage<T> FromData<T>(params T[] data)
        {
            return new MemoryDataStorage<T>(data);
        }
    }
}
