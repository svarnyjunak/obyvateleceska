using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Utils;

namespace SvarnyJunak.CeskeObce.Data.Repositories
{
    public class MunicipalityCache
    {
        private IEnumerable<Municipality> __municipalities;

        public MunicipalityCache(IMunicipalityRepository repository)
        {
            __municipalities = repository.GetMunicipalities();
        }

        /// <summary>
        /// Find municipality by its name, diacritic does not matter.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<Municipality> FindByName(string name)
        {
            return from m in __municipalities
                   where CompareWithoutDiacriticsIfNotProvided(name.ToUpper().Trim(), m.Name)
                   orderby m.Name
                   select m;
        }

        public IEnumerable<Municipality> FindByNameAndDistrict(string name, string district)
        {
            return from m in __municipalities
                   where CompareWithoutDiacriticsIfNotProvided(name.ToUpper().Trim(), m.Name) &&
                         CompareWithoutDiacriticsIfNotProvided(district.ToUpper().Trim(), m.DistrictName)
                   orderby m.Name
                   select m;
        }

        private bool CompareWithoutDiacriticsIfNotProvided(string userString, string municipalityName)
        {
            if (userString.HasDiacritics())
                return municipalityName.ToUpper().StartsWith(userString);

            return municipalityName.ToUpper().RemoveDiacritics().StartsWith(userString.RemoveDiacritics());
        }

        public bool Contains(string code)
        {
            return __municipalities.Any(m => m.Code == code);
        }

        public Municipality GetMunicipality(string code)
        {
            if (code == null)
                throw new ArgumentNullException("code");

            var result = __municipalities.SingleOrDefault(m => m.Code == code);

            if (result == null)
                throw new MunicipalityNotFoundException(String.Format("Municipality with given code {0} was not found.", code));

            return result;
        }

        public Municipality GetRandomMunicipality()
        {
            return __municipalities.GetRandomElement();
        }
    }
}
