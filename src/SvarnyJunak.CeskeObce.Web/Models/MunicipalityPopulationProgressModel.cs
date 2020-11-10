using SvarnyJunak.CeskeObce.Data.Entities;

namespace SvarnyJunak.CeskeObce.Web.Models
{
    public record MunicipalityPopulationProgressModel(Municipality Municipality, PopulationFrame[] PopulationProgress);
}
