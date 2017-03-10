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
            : base(String.Format("Municipality with name {0} was not found.", municipalityName))
        {
            MunicipalityName = municipalityName;
        }

        public string MunicipalityName { get; set; }
    }
}
