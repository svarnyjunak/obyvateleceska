using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvarnyJunak.CeskeObce.Data.Utils
{
    /*
    public class BinaryDataSerializer : IDataSerializer
    {
        private readonly IFormatter __formatter = new BinaryFormatter();

        public string GetDataExtension()
        {
            return "dat";
        }

        public byte[] Write<T>(T data)
        {
            using (var stream = new MemoryStream())
            {
                __formatter.Serialize(stream, data);
                return stream.ToArray();
            }
        }

        public T Read<T>(byte[] data)
        {
            T deserializedObject;

            using (var memoryStream = new MemoryStream(data))
            {
                deserializedObject = (T)__formatter.Deserialize(memoryStream);
            }

            return deserializedObject;
        }
    }
    */
}
