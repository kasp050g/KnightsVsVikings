using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.ExtensionMethodsTest;
using UnitTesting.Lucas_Testing.FieldFromLambda;

namespace UnitTesting.Lucas_Testing.FieldFromLambda
{
    [TestClass]
    public class FieldLambda
    {
        [TestMethod]
        public void GetFieldFromClassNoBindingFlags()
        {
            Dictionary<string, Type> actual = typeof(FieldClass).GetFields()
                                                                .ToDictionary(field => field.Name, field => field.FieldType);

            Dictionary<string, Type> expected = new Dictionary<string, Type>
            {
                {"Name", typeof(string) }
            };

            Assert.IsTrue(expected.IsEqualsToDictionary(actual));
        }

        [TestMethod]
        public void GetPropertyFromClassNoBindingClass()
        {
            Dictionary<string, Type> actual = typeof(FieldClass).GetProperties()
                                                                .ToDictionary(field => field.Name, field => field.PropertyType);

            Dictionary<string, Type> expected = new Dictionary<string, Type>
            {
                {"Age", typeof(int) }
            };

            Assert.IsTrue(expected.IsEqualsToDictionary(actual));
        }
    }
}
