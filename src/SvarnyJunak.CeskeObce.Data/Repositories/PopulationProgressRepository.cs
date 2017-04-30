using SvarnyJunak.CeskeObce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SvarnyJunak.CeskeObce.Data.Repositories
{
    public class PopulationProgressRepository
    {
        private IEnumerable<PopulationProgressInMunicipality> _populationProgress;

        public PopulationProgressRepository(IDataLoader dataLoader)
        {
            _populationProgress = dataLoader.GetPopulationProgress();
        }

        public PopulationProgressInMunicipality GetByMunicipalityCode(string municipalityCode)
        {
            var result = (from p in _populationProgress where p.MunicipalityCode == municipalityCode select p).ToArray();

            if (!result.Any())
                throw new ArgumentException("No result found for given municipalityCode.");

            if (result.Count() > 1)
                throw new ArgumentException("Too many records found for given municipalityCode.");

            return result.Single();
        }
    }
}
