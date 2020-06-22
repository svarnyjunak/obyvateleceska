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
    public class JsonDataStorage : IMunicipalityDataStorer
    {
        private readonly IDataSerializer _serializer;
        private readonly string _path;
        private readonly IFileStorage _fileStorage;

        public JsonDataStorage(IDataSerializer serializer, string path)
        {
            _serializer = serializer;
            _path = path;
            _fileStorage = new FileStorage();
        }

        public JsonDataStorage(IDataSerializer serializer, string path, IFileStorage fileStorage)
        {
            _serializer = serializer;
            _path = path;
            _fileStorage = fileStorage;
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
            var path = Path.Combine(_path, dataName + "." + _serializer.GetDataExtension());
            _fileStorage.Store(path, _serializer.Write(data));
        }
    }
}
