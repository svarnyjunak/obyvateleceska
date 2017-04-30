using System;
using System.Collections.Generic;
using System.Linq;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Utils;

namespace SvarnyJunak.CeskeObce.Data.Repositories.SerializedJson
{
    public class JsonDataLoader : IDataLoader
    {
        static JsonDataLoader()
        {
            Init();
        }

        protected static void Init()
        {
            var dataLoader = new JsonResources();
            var serializer = new JsonDataSerializer();
            CachedMunicipalities = serializer.Read<Municipality[]>(dataLoader.LoadMunicipalities());
            CachedPopulationProggress = serializer.Read<IEnumerable<PopulationProgressInMunicipality>>(dataLoader.LoadProgress());
        }

        protected static IEnumerable<Municipality> CachedMunicipalities { get; set; }
        protected static IEnumerable<PopulationProgressInMunicipality> CachedPopulationProggress { get; set; }

        public IEnumerable<Municipality> GetMunicipalities()
        {
            return CachedMunicipalities.ToArray();
        }

        public IEnumerable<PopulationProgressInMunicipality> GetPopulationProgress()
        {
            return CachedPopulationProggress.ToArray();
        }
    }
}
