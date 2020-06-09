using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.ExtensionMethodsTest;
using UnitTesting.Lucas_Testing.RemoveLambda.Classes;

namespace UnitTesting.Lucas_Testing.RemoveLambda
{
    [TestClass]
    public class PropertiesTestA
    {
        [TestMethod]
        public void RemoveBasePropertiesFromProperties()
        {
            List<PropertyInfo> properties = typeof(Inherit).GetProperties().ToList();
            List<PropertyInfo> baseProperties = typeof(BaseR).GetProperties().Where(property => property.Name != "Id").ToList();

            List<PropertyInfo> expected = properties.Where(property => property.Name != "RemoveMe").ToList();

            properties.RemoveAll(property => baseProperties.Exists(baseProperty => baseProperty.Name == property.Name));

            expected = expected.OrderBy(property => property.Name != "Id").ToList();
            properties = properties.OrderBy(property => property.Name != "Id").ToList();

            Assert.IsTrue(expected.IsEqualsToList(properties));
        }
    }
}
