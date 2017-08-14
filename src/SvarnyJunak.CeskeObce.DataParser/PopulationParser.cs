using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvarnyJunak.CeskeObce.Data;
using SvarnyJunak.CeskeObce.DataParser.Entities;
using SvarnyJunak.CeskeObce.DataParser.Utils;
using DataRow = SvarnyJunak.CeskeObce.DataParser.Utils.DataRow;

namespace SvarnyJunak.CeskeObce.DataParser
{
    public sealed class PopulationParser : ParserBase
    {
        public IEnumerable<PopulationInMunicipalitity> Parse(DataRow[] rows, int year)
        {
            foreach (DataRow row in rows)
            {
                var teritoryCode = row.Columns[0] as String;
                if (teritoryCode == null)
                    continue;

                if (!teritoryCode.StartsWith("CZ"))
                    continue;

                yield return Parse(row, year);
            }
        }

        private PopulationInMunicipalitity Parse(DataRow row, int year)
        {
            return new PopulationInMunicipalitity
            {
                MunicipalityCode = Parser.ParseString(row.Columns[1]),
                Year = year,
                TotalCount = Parser.ParseInt(row.Columns[3]),
                MalesCount = Parser.ParseInt(row.Columns[4]),
                FemalesCount = Parser.ParseInt(row.Columns[5]),
                AverageAge = Parser.ParseDecimal(row.Columns[6]),
                MaleAverageAge = Parser.ParseDecimal(row.Columns[7]),
                FemaleAverageAge = Parser.ParseDecimal(row.Columns[8]),
            };
        }
    }
}
