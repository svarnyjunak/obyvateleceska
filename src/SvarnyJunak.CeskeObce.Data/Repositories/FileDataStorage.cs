using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SvarnyJunak.CeskeObce.Data.Repositories
{
    public class FileDataStorage<T> : IDataStorage<T>
    {
        private string path;

        public FileDataStorage(string path)
        {
            this.path = path;
        }

        public T[] Load()
        {
            using FileStream createStream = File.OpenRead(GetFileName());
            return JsonSerializer.Deserialize<T[]>(createStream) ?? Array.Empty<T>();
        }

        public async Task StoreAsync(T[] data)
        {
            var json = JsonSerializer.Serialize(data);
            await File.WriteAllTextAsync(GetFileName(), json);
        }

        private string GetFileName()
        {
            var type = typeof(T);
            return Path.Combine(path, $"{type.Name}.json"); 
        }
    }
}
