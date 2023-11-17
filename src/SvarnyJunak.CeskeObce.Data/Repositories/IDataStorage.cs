using System.Threading.Tasks;

namespace SvarnyJunak.CeskeObce.Data.Repositories
{
    public interface IDataStorage<T>
    {
        Task StoreAsync(T[] data);

        T[] Load();
    }
}
