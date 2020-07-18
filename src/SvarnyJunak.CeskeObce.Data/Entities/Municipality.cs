using System;
using System.Collections.Generic;

namespace SvarnyJunak.CeskeObce.Data.Entities
{
    public class Municipality
    {
        public string MunicipalityId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string DistrictName { get; set; } = default!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public List<PopulationFrame> PopulationProgress { get; set; }=new List<PopulationFrame>();
    }
}
