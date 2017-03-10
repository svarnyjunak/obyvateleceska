using System;

namespace SvarnyJunak.CeskeObce.Data.Entities
{
    public class Municipality
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string DistrictName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
