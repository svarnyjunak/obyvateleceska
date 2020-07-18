using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Utils;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;

namespace SvarnyJunak.CeskeObce.Data.Repositories
{
    public interface IMunicipalityRepository : IRepository<Municipality>
    {
        Municipality GetByCode(string code);
        Municipality GetRandom();
    }

    public class MunicipalityRepository : RepositoryBase<Municipality>, IMunicipalityRepository
    {
        public MunicipalityRepository(CeskeObceDbContext dbContext) : base(dbContext)
        {
        }

        public Municipality GetByCode(string code)
        {
            var result = DbContext.Municipalities.SingleOrDefault(m => m.MunicipalityId == code);

            if (result == null)
                throw new MunicipalityNotFoundException($"Municipality with given code {code} was not found.");

            return result;
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
