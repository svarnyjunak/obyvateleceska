using OfficeOpenXml;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.DataParser.Parsers;
using SvarnyJunak.CeskeObce.DataParser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var csuParser = new CsuPopulationParser();

            population.AddPopulationData(GetData("./Data/population_2004.xlsx", sheetName), csuParser, 2004);
            population.AddPopulationData(GetData("./Data/population_2005.xlsx", sheetName), csuParser, 2005);
            population.AddPopulationData(GetData("./Data/population_2006.xlsx", sheetName), csuParser, 2006);
            population.AddPopulationData(GetData("./Data/population_2007.xlsx", sheetName), csuParser, 2007);
            population.AddPopulationData(GetData("./Data/population_2008.xlsx", sheetName), csuParser, 2008);
            population.AddPopulationData(GetData("./Data/population_2009.xlsx", sheetName), csuParser, 2009);
            population.AddPopulationData(GetData("./Data/population_2010.xlsx", sheetName), csuParser, 2010);
            population.AddPopulationData(GetData("./Data/population_2011.xlsx", sheetName), csuParser, 2011);
            population.AddPopulationData(GetData("./Data/population_2012.xlsx", sheetName), csuParser, 2012);
            population.AddPopulationData(GetData("./Data/population_2013.xlsx", sheetName), csuParser, 2013);
            population.AddPopulationData(GetData("./Data/population_2014.xlsx", sheetName), csuParser, 2014);
            population.AddPopulationData(GetData("./Data/population_2015.xlsx", sheetName), csuParser, 2015);
            population.AddPopulationData(GetData("./Data/population_2016.xlsx", sheetName), csuParser, 2016);
            population.AddPopulationData(GetData("./Data/population_2017.xlsx", sheetName), csuParser, 2017);
            population.AddPopulationData(GetData("./Data/population_2018.xlsx", sheetName), csuParser, 2018);
            population.AddPopulationData(GetData("./Data/population_2019.xlsx", sheetName), csuParser, 2019);
            population.AddPopulationData(GetData("./Data/population_2020.xlsx", sheetName), csuParser, 2020);
            population.AddPopulationData(GetData("./Data/population_2021.xlsx", sheetName), csuParser, 2021);
            population.AddPopulationData(GetData("./Data/population_2022.xlsx", sheetName), csuParser, 2022);
            population.AddPopulationData(GetData("./Data/population_2023.xlsx", sheetName), csuParser, 2023);
            population.AddPopulationData(GetData("./Data/population_2024.xlsx", sheetName), csuParser, 2024);

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
