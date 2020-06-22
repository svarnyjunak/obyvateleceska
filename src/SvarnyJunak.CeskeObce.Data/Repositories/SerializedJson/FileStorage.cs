using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SvarnyJunak.CeskeObce.Data.Repositories.SerializedJson
{
    public interface IFileStorage
    {
        void Store(string path, string content);
    }

    public class FileStorage : IFileStorage
    {
        public void Store(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
