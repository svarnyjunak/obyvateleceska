using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvarnyJunak.CeskeObce.Data.Repositories
{
    public class MunicipalityNotFoundException : Exception
    {
        public MunicipalityNotFoundException(string municipalityName)
            : base($"Municipality with name {municipalityName} was not found.")
        {
            MunicipalityName = municipalityName;
        }

        public string MunicipalityName { get; set; }
    }
}
