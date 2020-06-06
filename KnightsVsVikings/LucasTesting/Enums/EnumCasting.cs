using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucasTesting.Enums
{
    [TestClass]
    public class EnumCasting
    {
        [TestMethod]
        public void EnumCast()
        {
            EnumBase a = EnumBase.Bob;
            EnumBase b = (EnumBase)0;

            Assert.IsTrue(a == b);
        }
    }
}
