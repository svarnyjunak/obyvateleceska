using SvarnyJunak.CeskeObce.DataParser.Entities;
using SvarnyJunak.CeskeObce.DataParser.Utils;
using System.Collections.Generic;
using System.Linq;

namespace SvarnyJunak.CeskeObce.DataParser.Parsers
{
    public class MvcrPopulationParser : ParserBase, IPopulationParser
    {
        public IEnumerable<PopulationInMunicipalitity> Parse(DataRow[] rows, int year)
        {
            foreach (DataRow row in rows.Skip(6))
            {
                yield return new PopulationInMunicipalitity
                {
                    MunicipalityCode = Parser.ParseString(row.Columns[3]),
                    Year = year,
                    TotalCount = Parser.ParseInt(row.Columns[9]),
                    MalesCount = Parser.ParseInt(row.Columns[5]),
                    FemalesCount = Parser.ParseInt(row.Columns[7]),
                };
            }
        }
    }
}
