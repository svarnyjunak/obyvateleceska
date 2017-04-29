using SvarnyJunak.CeskeObce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SvarnyJunak.CeskeObce.Data.Repositories.Queries
{
    public abstract class QueryMunicipality : IQuery<Municipality>
    {
        public abstract Expression<Func<Municipality, bool>> Expression { get; }
    }
}
