using SvarnyJunak.CeskeObce.Data.Repositories;
using Xunit;

namespace SvarnyJunak.CeskeObce.Data.Test.Repositories
{
    public class MunicipalityNotFoundExceptionTest
    {
        [Fact]
        public void Constructor_Test()
        {
            var ex = new MunicipalityNotFoundException("test");
            Assert.Equal("test", ex.MunicipalityName);
        }
    }
}
