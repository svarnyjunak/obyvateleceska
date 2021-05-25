using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SvarnyJunak.CeskeObce.Data.Utils;

namespace SvarnyJunak.CeskeObce.Data.Test.Utils
{
    [TestClass]
    public class StringUtilsTest
    {
        [TestMethod]
        public void RemoveDiacritics_Test()
        {
            var text = "Příliš žluťoučký kůň úpěl ďábelské ódy.";
            Assert.AreEqual("Prilis zlutoucky kun upel dabelske ody.", text.RemoveDiacritics());
        }

        [TestMethod]
        public void HasDiacritics_PositiveTest()
        {
            var text = "Příliš žluťoučký kůň úpěl ďábelské ódy.";
            Assert.IsTrue(text.HasDiacritics());
        }

        [TestMethod]
        public void HasDiacritics_NegativeTest()
        {
            var text = "Text bez diakritiky.";
            Assert.IsFalse(text.HasDiacritics());
        }

        [TestMethod]

        public void CompareWithoutDiacriticsIfNotProvided_PositiveTest()
        {
            var text = "Příliš žluťoučký kůň úpěl ďábelské ódy.";
            Assert.IsTrue("Prilis zlutoucky kun upel dabelske ody.".CompareWithoutDiacriticsIfNotProvided(text));
        }

        [TestMethod]

        public void CompareWithoutDiacriticsIfNotProvided_NegativeTest()
        {
            var text = "Příliš žluťoučký kůň úpěl ďábelské ódy.";
            Assert.IsFalse(text.CompareWithoutDiacriticsIfNotProvided("Prilis zlutoucky kun upel dabelske ody."));
        }

        [TestMethod]
        public void ToUrlSegment()
        {
            var text = "Příliš žluťoučký kůň úpěl ďábelské ódy";
            Assert.AreEqual("prilis-zlutoucky-kun-upel-dabelske-ody", text.ToUrlSegment());
        }
    }
}
