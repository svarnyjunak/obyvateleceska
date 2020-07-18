using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SvarnyJunak.CeskeObce.Data.Utils;

namespace SvarnyJunak.CeskeObce.Data.Test.Utils
{
    [TestClass]
    public class CollectionUtilsTest
    {
        [TestMethod]
        public void GetRandomElement_FromList()
        {
            var list = new List<int>{1, 2, 3, 4, 5, 6}.AsQueryable();
            Assert.IsTrue(list.Contains(list.GetRandomElement()));
        }

        [TestMethod]
        public void GetRandomElement_FromArray()
        {
            var list = new int[] {1, 2, 3, 4, 5, 6}.AsQueryable();
            Assert.IsTrue(list.ToList().Contains(list.GetRandomElement()));
        }

        [TestMethod]
        public void GetRandomElement_EmptyCollection()
        {
            var list = new List<int>().AsQueryable();
            Assert.AreEqual(0, list.GetRandomElement());
        }
    }
}
