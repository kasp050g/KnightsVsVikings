using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting.Lucas_Testing.PropToVar
{
    [TestClass]
    public class TestPTV
    {
        [TestMethod]
        public void PropToVarTest()
        {
            int x = 1;
            int y = 2;

            Assert.AreEqual(x, y);
        }
    }
}
