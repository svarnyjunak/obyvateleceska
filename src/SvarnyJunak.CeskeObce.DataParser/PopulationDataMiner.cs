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

        public IEnumerable<PopulationProgressInMunicipality> ComputePopulationProgressInMunicipalities()
        {
            var municipalities = (from p in __dataList select p.MunicipalityCode).Distinct();

            foreach (var municipalityCode in municipalities)
            {
                var progress = GetPopulationProggress(municipalityCode);

                yield return new PopulationProgressInMunicipality
                {
                    MunicipalityCode = municipalityCode,
                    PopulationProgress = progress.ToArray()
                };
            }
        }

        public PopulationProgressInMunicipality FindMunicipalityWithBiggestPopulationGrowth()
        {
            var municipalities = (from p in __dataList select p.MunicipalityCode).Distinct();
            PopulationProgressInMunicipality result = null;

            foreach (var municipalityCode in municipalities)
            {
                var progress = GetPopulationProggress(municipalityCode);
                var tempMunicipality = new PopulationProgressInMunicipality
                {
                    MunicipalityCode = municipalityCode,
                    PopulationProgress = progress.ToArray()
                };

                if (result == null || HasBiggerPopulationGrowthRate(result, tempMunicipality))
                {
                    result = tempMunicipality;
                }
            }

            return result;
        }

        private bool HasBiggerPopulationGrowthRate(PopulationProgressInMunicipality a, PopulationProgressInMunicipality b)
        {
            return GetPopulationGrowthRate(a) < GetPopulationGrowthRate(b);
        }

        private int GetPopulationGrowthRate(PopulationProgressInMunicipality populationProgress)
        {
            var minYear = populationProgress.PopulationProgress.Min(d => d.Year);
            var minYearCount = (from d in populationProgress.PopulationProgress where d.Year == minYear select d.Count).Single();

            var maxYear = populationProgress.PopulationProgress.Max(d => d.Year);
            var maxYearCount = (from d in populationProgress.PopulationProgress where d.Year == maxYear select d.Count).Single();

            return maxYearCount / minYearCount;
        }

        private IEnumerable<PopulationFrame> GetPopulationProggress(string municipalityCode)
        {
            return from p in __dataList
                   where p.MunicipalityCode == municipalityCode
                   select new PopulationFrame
                   {
                       Year = p.Year,
                       Count = p.TotalCount,
                   };
        }
    }
}
