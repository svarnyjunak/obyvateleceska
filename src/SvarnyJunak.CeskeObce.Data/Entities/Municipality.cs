using System;

namespace SvarnyJunak.CeskeObce.Data.Entities
{
    public class Municipality
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string DistrictName { get; set; } = default!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
