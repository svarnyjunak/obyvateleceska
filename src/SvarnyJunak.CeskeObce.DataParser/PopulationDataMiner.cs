﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.DataParser.Entities;
using SvarnyJunak.CeskeObce.DataParser.Parsers;
using DataRow = SvarnyJunak.CeskeObce.DataParser.Utils.DataRow;

namespace SvarnyJunak.CeskeObce.DataParser
{
    public class PopulationDataMiner
    {
        private readonly List<PopulationInMunicipality> __dataList = new List<PopulationInMunicipality>();

        public void AddPopulationData(DataRow[] rows, IPopulationParser parser, int year)
        {
            __dataList.AddRange(parser.Parse(rows, year));
        }

        public void AddPopulationData(IEnumerable<PopulationInMunicipality> data)
        {
            __dataList.AddRange(data);
        }

        public IEnumerable<PopulationFrame> ComputePopulationProgressInMunicipalities()
        {
            var municipalities = (from p in __dataList select p.MunicipalityCode).Distinct();

            foreach (var municipalityCode in municipalities)
            {
                foreach (var frame in GetPopulationProggress(municipalityCode))
                {
                    yield return frame;
                }
            }
        }

        private IEnumerable<PopulationFrame> GetPopulationProggress(string municipalityCode)
        {
            return from p in __dataList
                   where p.MunicipalityCode == municipalityCode
                   select new PopulationFrame
                   {
                       MunicipalityId = municipalityCode,
                       Year = p.Year,
                       Count = p.TotalCount,
                   };
        }
    }
}
