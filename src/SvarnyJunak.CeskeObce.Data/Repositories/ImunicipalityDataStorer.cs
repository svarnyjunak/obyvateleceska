using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SvarnyJunak.CeskeObce.Data.Entities;

namespace SvarnyJunak.CeskeObce.Data.Repositories
{
    public interface IMunicipalityDataStorer
    {
        void StoreMunicipalities(IEnumerable<Municipality> municipalities);
        void StorePopulationProgress(IEnumerable<PopulationProgressInMunicipality> populationProgressInMunicipalities);
    }
}
