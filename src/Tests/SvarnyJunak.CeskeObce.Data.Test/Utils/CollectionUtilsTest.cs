using SvarnyJunak.CeskeObce.Data.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SvarnyJunak.CeskeObce.Data.Test.Utils
{
    public class CollectionUtilsTest
    {
        [Fact]
        public void GetRandomElement_FromList()
        {
            var list = new List<int>{1, 2, 3, 4, 5, 6}.AsQueryable();
            Assert.True(list.Contains(list.GetRandomElement()));
        }

        [Fact]
        public void GetRandomElement_FromArray()
        {
            var list = new int[] {1, 2, 3, 4, 5, 6}.AsQueryable();
            Assert.Contains(list.GetRandomElement(), list.ToList());
        }

        [Fact]
        public void GetRandomElement_EmptyCollection()
        {
            var list = new List<int>().AsQueryable();
            Assert.Equal(0, list.GetRandomElement());
        }
    }
}
