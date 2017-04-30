using System.Collections.Generic;
using SvarnyJunak.CeskeObce.Data.Entities;

namespace SvarnyJunak.CeskeObce.Data.Repositories
{
    public interface IDataLoader
    {
        IEnumerable<Municipality> GetMunicipalities();
        IEnumerable<PopulationProgressInMunicipality> GetPopulationProgress();
    }
}