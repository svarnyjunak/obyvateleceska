using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Utils;

namespace SvarnyJunak.CeskeObce.Data.Repositories.SerializedJson
{
    public class MunicipalityDataStorer : IMunicipalityDataStorer
    {
        private readonly IDataSerializer __serializer;
        private readonly string __path;

        public MunicipalityDataStorer(IDataSerializer serializer, string path)
        {
            __serializer = serializer;
            __path = path;
        }

        public void StoreMunicipalities(IEnumerable<Municipality> municipalities)
        {
            Serialize(municipalities, "municipalities");
        }

        public void StorePopulationProgress(IEnumerable<PopulationProgressInMunicipality> populationProgressInMunicipalities)
        {
            Serialize(populationProgressInMunicipalities, "progress");
        }

        private void Serialize<T>(IEnumerable<T> data, string dataName) where T : class
        {
            var path = Path.Combine(__path, dataName + "." + __serializer.GetDataExtension());
            File.WriteAllText(path, __serializer.Write(data));
        }
    }
}
