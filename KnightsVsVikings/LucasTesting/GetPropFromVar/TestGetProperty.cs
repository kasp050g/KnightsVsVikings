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
    public class TestGetProperty
    {
        [TestMethod]
        public void GetPropertyTest()
        {
            PropertyClass propertyClass = new PropertyClass();

            PropertyInfo actual = PropertyFinder<PropertyClass>.Find(x => x.GetMe);
            PropertyInfo expected = propertyClass.GetType().GetProperty("GetMe");

            Assert.AreEqual(expected, actual);
        }
    }
}
