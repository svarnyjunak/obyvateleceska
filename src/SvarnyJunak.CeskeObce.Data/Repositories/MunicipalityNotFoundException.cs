using System;

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
