﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Equinor.ProCoSys.PO.MainApi.Tests
{
    [TestClass]
    public class MainApiOptionsTests
    {
        [TestMethod]
        public void TagSearchPageSize_Is100ByDefault()
        {
            var dut = new MainApiOptions();

            Assert.AreEqual(100, dut.TagSearchPageSize);
        }
    }
}
