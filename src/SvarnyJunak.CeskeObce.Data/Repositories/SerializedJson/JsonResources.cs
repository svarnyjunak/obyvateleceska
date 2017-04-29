using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SvarnyJunak.CeskeObce.Data.Repositories.SerializedJson
{
    public class JsonResources
    {
        public string LoadMunicipalities()
        {
            return LoadEmbededFile("Municipalities.js");
        }

        public string LoadProgress()
        {
            return LoadEmbededFile("Progress.js");
        }

        private string LoadEmbededFile(string embededFileName)
        {
            var type = typeof(JsonResources);
            var assembly = type.GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream(type.Namespace + "." + embededFileName))
            using (var reader = new StreamReader(stream))
            {
                var data = reader.ReadToEnd();
                return data;
            }
        }
    }
}
