using SvarnyJunak.CeskeObce.DataParser.Entities;
using SvarnyJunak.CeskeObce.DataParser.Utils;
using System.Collections.Generic;

namespace SvarnyJunak.CeskeObce.DataParser.Parsers
{
    public interface IPopulationParser
    {
        IEnumerable<PopulationInMunicipalitity> Parse(DataRow[] rows, int year);
    }
}
