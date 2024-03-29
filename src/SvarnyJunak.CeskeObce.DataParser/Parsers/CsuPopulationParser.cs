﻿using System;
using System.Collections.Generic;
using SvarnyJunak.CeskeObce.DataParser.Entities;
using DataRow = SvarnyJunak.CeskeObce.DataParser.Utils.DataRow;

namespace SvarnyJunak.CeskeObce.DataParser.Parsers
{
    public sealed class CsuPopulationParser : ParserBase, IPopulationParser
    {
        public IEnumerable<PopulationInMunicipality> Parse(DataRow[] rows, int year)
        {
            foreach (DataRow row in rows)
            {
                var territoryCode = row.Columns[0] as string;
                if (territoryCode == null)
                    continue;

                if (!territoryCode.StartsWith("CZ"))
                    continue;

                yield return Parse(row, year);
            }
        }

        private PopulationInMunicipality Parse(DataRow row, int year)
        {
            return new PopulationInMunicipality
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
