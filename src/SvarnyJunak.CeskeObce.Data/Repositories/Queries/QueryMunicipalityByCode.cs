using SvarnyJunak.CeskeObce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SvarnyJunak.CeskeObce.Data.Repositories.Queries
{
    public class QueryMunicipalityByCode : QueryMunicipality
    {
        public string Code { get; set; } = default!;

        public override Expression<Func<Municipality, bool>> Expression =>
            m => m.MunicipalityId == Code;
    }
}
