using SvarnyJunak.CeskeObce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SvarnyJunak.CeskeObce.Web.Models
{
    public class MunicipalityPopulationProgressModel
    {
        public Municipality Municipality { get; set; }
        public PopulationFrame[] PopulationProgress { get; set; }
    }
}
