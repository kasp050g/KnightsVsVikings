using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Lucas_Testing.SetFieldReflection.Classes;

namespace UnitTesting.Lucas_Testing.SetFieldReflection
{
    [TestClass]
    public class PropertySetValueReflectionTest
    {
        [TestMethod]
        public void SetPropertyTo()
        {
            IBase bob = new ModelBob();

            foreach (PropertyInfo property in bob.GetType().GetProperties())
                switch (property.Name.ToString())
                {
                    case "Name":
                        property.SetValue(bob, "Bob");
                        break;

                    case "Age":
                        property.SetValue(bob, 25);
                        break;
                }

            string expectedName = "Bob";
            string actualName = (bob as ModelBob).Name;

            int expectedAge = 25;
            int actualAge = (bob as ModelBob).Age;

            Assert.IsTrue(expectedName == actualName && expectedAge == actualAge);
        }
    }
}
