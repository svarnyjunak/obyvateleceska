using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using SvarnyJunak.CeskeObce.Data.Entities;

namespace SvarnyJunak.CeskeObce.Data.Repositories.Queries
{
    public class QueryPopulationFrameByMunicipalityCode : IQuery<PopulationFrame>
    {
        public string Code { get; set; } = default!;

        public Expression<Func<PopulationFrame, bool>> Expression =>
            m => m.MunicipalityId == Code;
    }
}
