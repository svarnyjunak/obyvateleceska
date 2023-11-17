namespace SvarnyJunak.CeskeObce.DataParser.Entities
{
    public class PopulationInMunicipality
    {
        public int Year { get; set; }
        public int TotalCount { get; set; }
        public int MalesCount { get; set; }
        public int FemalesCount { get; set; }

        public decimal AverageAge { get; set; }
        public decimal MaleAverageAge { get; set; }
        public decimal FemaleAverageAge { get; set; }

        public string MunicipalityCode { get; set; } = default!;
    }
}
