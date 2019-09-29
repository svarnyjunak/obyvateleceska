
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace SvarnyJunak.CeskeObce.Data.Utils
{
    public class JsonDataSerializer : IDataSerializer
    {
        public string GetDataExtension()
        {
            return "js";
        }

        public string Write<T>(T data)
        {
            return JsonSerializer.Serialize<T>(data);
        }

        public T Read<T>(string data)
        {
            return JsonSerializer.Deserialize<T>(data);
        }
    }
}
