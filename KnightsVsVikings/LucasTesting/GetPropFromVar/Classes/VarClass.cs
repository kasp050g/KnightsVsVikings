using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Lucas_Testing.SetFieldReflection.Classes;

namespace LucasTesting.GetPropFromVar.Classes
{
    public class VarClass : IBaseVarClass
    {
        public int GetMe { get; set; }
        public int GetMeToo { get; set; }
    }
}
