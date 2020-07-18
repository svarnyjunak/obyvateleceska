using SvarnyJunak.CeskeObce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SvarnyJunak.CeskeObce.Data.Repositories.Queries
{
    public abstract class QueryMunicipality : IQuery<Municipality>
    {
        public abstract Expression<Func<Municipality, bool>> Expression { get; }

        public IQueryable<Municipality> Order(IQueryable<Municipality> query)
        {
            return query.OrderBy(m => m.Name);
        }
    }
}
