using System.Collections.Generic;
using System.Linq;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Utils;
using Microsoft.EntityFrameworkCore;

namespace SvarnyJunak.CeskeObce.Data.Repositories
{
    public interface IMunicipalityRepository : IRepository<Municipality>
    {
        Municipality GetByCode(string code);
        IEnumerable<Municipality> GetClosests(decimal longitude, decimal latitude, int count = 5);
        Municipality GetRandom();
    }

    public class MunicipalityRepository : RepositoryBase<Municipality>, IMunicipalityRepository
    {
        public MunicipalityRepository(CeskeObceDbContext dbContext) : base(dbContext)
        {
        }

        public Municipality GetByCode(string code)
        {
            var result = DbContext.Municipalities.Find(code);

            if (result == null)
                throw new MunicipalityNotFoundException($"Municipality with given code {code} was not found.");

            return result;
        }

        public IEnumerable<Municipality> GetClosests(decimal longitude, decimal latitude, int count = 5)
        {
            return DbContext.Municipalities
                .OrderBy(m => GeoCoordinateUtils.GetDistance(longitude, latitude, m.Longitude, m.Latitude))
                .Take(count);
        }

        public Municipality GetRandom()
        {
            return DbContext.Municipalities.GetRandomElement();
        }

        protected override DbSet<Municipality> GetDbSet()
        {
            return DbContext.Municipalities;
        }
    }
}
