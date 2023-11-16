using SvarnyJunak.CeskeObce.Data.Utils;
using Xunit;

namespace SvarnyJunak.CeskeObce.Data.Test.Utils
{
    public class StringUtilsTest
    {
        [Fact]
        public void RemoveDiacritics_Test()
        {
            var text = "Příliš žluťoučký kůň úpěl ďábelské ódy.";
            Assert.Equal("Prilis zlutoucky kun upel dabelske ody.", text.RemoveDiacritics());
        }

        [Fact]
        public void HasDiacritics_PositiveTest()
        {
            var text = "Příliš žluťoučký kůň úpěl ďábelské ódy.";
            Assert.True(text.HasDiacritics());
        }

        [Fact]
        public void HasDiacritics_NegativeTest()
        {
            var text = "Text bez diakritiky.";
            Assert.False(text.HasDiacritics());
        }

        [Fact]

        public void CompareWithoutDiacriticsIfNotProvided_PositiveTest()
        {
            var text = "Příliš žluťoučký kůň úpěl ďábelské ódy.";
            Assert.True("Prilis zlutoucky kun upel dabelske ody.".CompareWithoutDiacriticsIfNotProvided(text));
        }

        [Fact]

        public void CompareWithoutDiacriticsIfNotProvided_NegativeTest()
        {
            var text = "Příliš žluťoučký kůň úpěl ďábelské ódy.";
            Assert.False(text.CompareWithoutDiacriticsIfNotProvided("Prilis zlutoucky kun upel dabelske ody."));
        }

        [Fact]
        public void ToUrlSegment()
        {
            var text = "Příliš žluťoučký kůň úpěl ďábelské ódy";
            Assert.Equal("prilis-zlutoucky-kun-upel-dabelske-ody", text.ToUrlSegment());
        }
    }
}
