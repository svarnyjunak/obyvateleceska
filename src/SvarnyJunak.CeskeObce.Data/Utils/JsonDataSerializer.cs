using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            return JsonConvert.SerializeObject(data);
        }

        public T Read<T>(string data)
        {
            var deserializedData = JsonConvert.DeserializeObject(data);
            return ((Newtonsoft.Json.Linq.JArray)deserializedData).ToObject<T>();
        }
    }
}
