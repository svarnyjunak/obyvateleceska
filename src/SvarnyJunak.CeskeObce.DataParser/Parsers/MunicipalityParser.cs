using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.DataParser.Utils;
using System.Collections.Generic;
using System.Linq;

namespace SvarnyJunak.CeskeObce.DataParser.Parsers
{
    public class MunicipalityParser : ParserBase
    {
        public IEnumerable<Municipality> Parse(DataRow[] data)
        {
            return data.Skip(1).Select(Parse);
        }

        private Municipality Parse(DataRow row)
        {
            return new Municipality
            {
                MunicipalityId = Parser.ParseString(row.Columns[0]),
                Name = Parser.ParseString(row.Columns[1]),
                DistrictName = Parser.ParseString(row.Columns[2]),
                Latitude = Parser.ParseDecimal(row.Columns[3]),
                Longitude = Parser.ParseDecimal(row.Columns[4]),
            };
        }
    }
}
