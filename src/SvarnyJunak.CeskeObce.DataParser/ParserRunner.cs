using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.DataParser.Utils;
using OfficeOpenXml;
using DataRow = SvarnyJunak.CeskeObce.DataParser.Utils.DataRow;

namespace SvarnyJunak.CeskeObce.DataParser
{
    public sealed class ParserRunner
    {
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly IPopulationFrameRepository _populationFrameRepository;

        public ParserRunner(IMunicipalityRepository municipalityRepository, IPopulationFrameRepository populationFrameRepository)
        {
            _municipalityRepository = municipalityRepository;
            _populationFrameRepository = populationFrameRepository;
        }

        public async Task Run()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var municipalities = GetMunicipalities().ToArray();
            var populationFrames = GetPopulationProgress().ToArray();

            var missingMunicipalities = populationFrames
                .Where(p => municipalities.All(m => m.MunicipalityId != p.MunicipalityId))
                .GroupBy(p => p.MunicipalityId)
                .Select(p => $"{p.Key} year {p.Max(p => p.Year)}")
                .ToArray();

            if (missingMunicipalities.Any())
            {
                var message = "Any municipalities are missing " + String.Join(", ", missingMunicipalities);
                throw new Exception(message);
            }

            await _municipalityRepository.ReplaceAllAsync(municipalities);
            await _populationFrameRepository.ReplaceAllAsync(populationFrames);
        }

        private IEnumerable<Municipality> GetMunicipalities()
        {
            var parser = new MunicipalityParser();
            var data = GetData("./Data/municipalities.xlsx", "municipalities").ToArray();
            var municipalities = parser.Parse(data);
            return municipalities;
        }

        private IEnumerable<PopulationFrame> GetPopulationProgress()
        {
            const string sheetName = "List1";
            var population = new PopulationDataMiner();

            population.AddPopulationData(GetData("./Data/population_2004.xlsx", sheetName), 2004);
            population.AddPopulationData(GetData("./Data/population_2005.xlsx", sheetName), 2005);
            population.AddPopulationData(GetData("./Data/population_2006.xlsx", sheetName), 2006);
            population.AddPopulationData(GetData("./Data/population_2007.xlsx", sheetName), 2007);
            population.AddPopulationData(GetData("./Data/population_2008.xlsx", sheetName), 2008);
            population.AddPopulationData(GetData("./Data/population_2009.xlsx", sheetName), 2009);
            population.AddPopulationData(GetData("./Data/population_2010.xlsx", sheetName), 2010);
            population.AddPopulationData(GetData("./Data/population_2011.xlsx", sheetName), 2011);
            population.AddPopulationData(GetData("./Data/population_2012.xlsx", sheetName), 2012);
            population.AddPopulationData(GetData("./Data/population_2013.xlsx", sheetName), 2013);
            population.AddPopulationData(GetData("./Data/population_2014.xlsx", sheetName), 2014);
            population.AddPopulationData(GetData("./Data/population_2015.xlsx", sheetName), 2015);
            population.AddPopulationData(GetData("./Data/population_2016.xlsx", sheetName), 2016);
            population.AddPopulationData(GetData("./Data/population_2017.xlsx", sheetName), 2017);
            population.AddPopulationData(GetData("./Data/population_2018.xlsx", sheetName), 2018);
            population.AddPopulationData(GetData("./Data/population_2019.xlsx", sheetName), 2019);
            population.AddPopulationData(GetData("./Data/population_2020.xlsx", sheetName), 2020);
            population.AddPopulationData(GetData("./Data/population_2021.xlsx", sheetName), 2021);

            var progress = population.ComputePopulationProgressInMunicipalities().ToArray();
            return progress;
        }

        private DataRow[] GetData(string file, string sheetName)
        {
            var excelReader = new ExcelReader();
            return excelReader.ReadData(file, sheetName).ToArray();
        }
    }
}
