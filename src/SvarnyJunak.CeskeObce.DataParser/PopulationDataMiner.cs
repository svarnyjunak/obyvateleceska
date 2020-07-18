using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SvarnyJunak.CeskeObce.Data;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.DataParser.Entities;
using SvarnyJunak.CeskeObce.DataParser.Utils;
using DataRow = SvarnyJunak.CeskeObce.DataParser.Utils.DataRow;

namespace SvarnyJunak.CeskeObce.DataParser
{
    public class PopulationDataMiner
    {
        private readonly List<PopulationInMunicipalitity> __dataList = new List<PopulationInMunicipalitity>();

        public void AddPopulationData(DataRow[] rows, int year)
        {
            var parser = new PopulationParser();
            __dataList.AddRange(parser.Parse(rows, year));
        }

        public void AddPopulationData(IEnumerable<PopulationInMunicipalitity> data)
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
