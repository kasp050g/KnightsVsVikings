using LucasTesting.ExtensionMethodsTest;
using LucasTesting.GetPropFromVar.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LucasTesting.GetPropFromVar
{
    [TestClass]
    public class TestVar
    {
        [TestMethod]
        public void PropVarTest()
        {
            VarClass varClass = new VarClass();

            PropertyInfo actual = PropertyFinder<VarClass>.Find(x => x.GetMe);
            PropertyInfo expected = varClass.GetType().GetProperty("GetMe");

            Assert.AreEqual(expected, actual);
        }
    }
}
