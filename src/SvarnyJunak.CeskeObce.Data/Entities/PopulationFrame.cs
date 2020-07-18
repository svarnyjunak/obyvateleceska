using System;

namespace SvarnyJunak.CeskeObce.Data.Entities
{
    public class PopulationFrame
    {
        public Guid PopulationFrameId { get; set; }

        public int Year { get; set; }
        public int Count { get; set; }

        public string MunicipalityId { get; set; }
        public Municipality Municipality { get; set; }
    }
}