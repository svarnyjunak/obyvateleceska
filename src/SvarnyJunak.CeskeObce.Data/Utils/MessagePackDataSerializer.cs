using System.IO;

namespace SvarnyJunak.CeskeObce.Data.Utils
{
    /*
    public class MessagePackDataSerializer : IDataSerializer
    {
        public string GetDataExtension()
        {
            return "mp";
        }

        public byte[] Write<T>(T data)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = SerializationContext.Default.GetSerializer<T>();
                serializer.Pack(stream, data);
                return stream.ToArray();
            }
        }

        public T Read<T>(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                var serializer = SerializationContext.Default.GetSerializer<T>();
                return serializer.Unpack(stream);                
            }
        }
    }
    */
}
