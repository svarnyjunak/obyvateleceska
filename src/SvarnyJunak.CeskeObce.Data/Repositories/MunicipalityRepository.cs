using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Utils;
using System.Linq.Expressions;
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;

namespace SvarnyJunak.CeskeObce.Data.Repositories
{
    public class MunicipalityRepository
    {
        private IEnumerable<Municipality> _municipalities;

        public MunicipalityRepository(IDataLoader dataLoader)
        {
            _municipalities = dataLoader.GetMunicipalities();
        }

        public Municipality GetByCode(string code)
        {
            if (code == null)
                throw new ArgumentNullException("code");

            var result = _municipalities.SingleOrDefault(m => m.Code == code);

            if (result == null)
                throw new MunicipalityNotFoundException(String.Format("Municipality with given code {0} was not found.", code));

            return result;
        }

        public Municipality GetRandom()
        {
            return _municipalities.GetRandomElement();
        }

        public IEnumerable<Municipality> FindAll(IQuery<Municipality> query)
        {
            return _municipalities.Where(query.Expression.Compile()).OrderBy(m => m.Name);
        }

        public bool Exists(IQuery<Municipality> query)
        {
            return _municipalities.Any(query.Expression.Compile());
        }
    }
}
