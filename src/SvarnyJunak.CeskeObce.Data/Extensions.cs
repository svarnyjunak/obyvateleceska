using Microsoft.Extensions.DependencyInjection;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories;

namespace SvarnyJunak.CeskeObce.Data
{
    public static class Extensions
    {
        public static IServiceCollection UseFileRepositories(this IServiceCollection services, string path)
        {
            var municipalityDataStorage = new FileDataStorage<Municipality>(path);
            var municipalityRepository = new MunicipalityRepository(municipalityDataStorage);

            var populationFrameDataStorage = new FileDataStorage<PopulationFrame>(path);
            var populationFrameRepository = new PopulationFrameRepository(populationFrameDataStorage);

            services.AddSingleton(typeof(IMunicipalityRepository), municipalityRepository);
            services.AddSingleton(typeof(IPopulationFrameRepository), populationFrameRepository);

            return services;
        }
    }
}
