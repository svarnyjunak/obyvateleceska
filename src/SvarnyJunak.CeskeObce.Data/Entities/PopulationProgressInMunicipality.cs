using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace SvarnyJunak.CeskeObce.Data.Entities
{
    public class PopulationProgressInMunicipality
    {
        /// <summary>
        /// Kód obce
        /// </summary>
        public string MunicipalityCode { get; set; }

        /// <summary>
        /// Počet obyvatel za sledované roky
        /// </summary>
        public IEnumerable<PopulationFrame> PopulationProgress { get; set; }
    }
}
