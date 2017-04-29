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

        // todo: Move this to repository
        public PopulationProgressInMunicipality GetPopulationProgress(string municipalityCode)
        {
            var result = (from p in CachedPopulationProggress where p.MunicipalityCode == municipalityCode select p).ToArray();

            if (!result.Any())
                throw new ArgumentException("No result found for given municipalityCode.");

            if (result.Count() > 1)
                throw new ArgumentException("Too many records found for given municipalityCode.");

            return result.Single();

        }
    }
}
