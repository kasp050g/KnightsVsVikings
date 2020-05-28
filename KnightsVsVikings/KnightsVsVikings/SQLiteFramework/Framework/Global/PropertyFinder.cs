using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Framework.Global
{
    public static class PropertyFinder<T> where T : ISQLiteRow
    {
        public static PropertyInfo Find<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return (PropertyInfo)((MemberExpression)expression.Body).Member;
        }
    }
}
