using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Utils;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SvarnyJunak.CeskeObce.Data.Repositories.Queries
{
    /// <summary>
    /// Find municipality by its name, diacritic does not matter.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public class QueryMunicipalityByName : QueryMunicipality
    {
        public string Name { get; set; }

        public override Expression<Func<Municipality, bool>> Expression =>
            m => Name.CompareWithoutDiacriticsIfNotProvided(m.Name);
    }
}
