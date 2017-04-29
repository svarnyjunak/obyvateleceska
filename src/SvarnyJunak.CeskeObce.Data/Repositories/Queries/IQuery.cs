using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SvarnyJunak.CeskeObce.Data.Repositories.Queries
{
    public interface IQuery<T>
    {
        Expression<Func<T, bool>> Expression { get; }
    }
}
