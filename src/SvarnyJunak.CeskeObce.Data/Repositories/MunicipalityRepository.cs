using System.Collections.Generic;
using System.Linq;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Utils;

namespace SvarnyJunak.CeskeObce.Data.Repositories
{
    public interface IMunicipalityRepository : IRepository<Municipality>
    {
        Municipality GetByCode(string code);
        IEnumerable<Municipality> GetClosest(decimal longitude, decimal latitude, int count = 5);
        Municipality GetRandom();
    }

    public class MunicipalityRepository : RepositoryBase<Municipality>, IMunicipalityRepository
    {
        public MunicipalityRepository(IDataStorage<Municipality> dataStorage) : base(dataStorage)
        {
        }

        public Municipality GetByCode(string code)
        {
            var result = Data.SingleOrDefault(d => d.MunicipalityId == code);

            if (result == null)
                throw new MunicipalityNotFoundException($"Municipality with given code {code} was not found.");

            return result;
        }

        public IEnumerable<Municipality> GetClosest(decimal longitude, decimal latitude, int count = 5)
        {
            return Data
                .OrderBy(m => GeoCoordinateUtils.GetDistance(longitude, latitude, m.Longitude, m.Latitude))
                .Take(count);
        }

        public Municipality GetRandom()
        {
            return Data.GetRandomElement();
        }
    }
}
