using SvarnyJunak.CeskeObce.Data.Entities;
using Xunit;

namespace SvarnyJunak.CeskeObce.Data.Test.Entities
{
    public class MunicipalityTest
    {
        [Fact]
        public void MunicipalityId_Test()
        {
            var municipality = new Municipality
            {
                MunicipalityId = "code"
            };

            Assert.Equal("code", municipality.MunicipalityId);
        }

        [Fact]
        public void DistrictName_Test()
        {
            var municipality = new Municipality
            {
                DistrictName = "district"
            };

            Assert.Equal("district", municipality.DistrictName);
        }

        [Fact]
        public void Latitude_Test()
        {
            var municipality = new Municipality
            {
                Latitude = 10.23m
            };

            Assert.Equal(10.23m, municipality.Latitude);
        }

        [Fact]
        public void Longitude_Test()
        {
            var municipality = new Municipality
            {
                Longitude = 20.23m
            };

            Assert.Equal(20.23m, municipality.Longitude);
        }
    }
}
