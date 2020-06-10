using LucasTesting.GetPropFromVar.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LucasTesting.ExtensionMethodsTest
{
    public static class PropertyFinder<T> where T : IBasePropertyClass
    {
        public static PropertyInfo Find<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return (PropertyInfo)((MemberExpression)expression.Body).Member;
        }
    }
}
