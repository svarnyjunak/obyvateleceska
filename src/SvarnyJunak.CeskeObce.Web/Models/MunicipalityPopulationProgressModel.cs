using SvarnyJunak.CeskeObce.Data.Entities;

namespace SvarnyJunak.CeskeObce.Web.Models
{
    public class MunicipalityPopulationProgressModel
    {
        public string MunicipalityNameSearch { get; set; } = default!;
        public Municipality Municipality { get; set; } = new Municipality();
        public PopulationFrame[] PopulationProgress { get; set; } = new PopulationFrame[0];
    }
}
