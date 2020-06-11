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
    // Lucas

    /// <summary>
    /// Finds the Property of a given ISQLiteRow
    /// </summary>
    /// <typeparam name="T">The SQLite Model to the find the Property of.</typeparam>
    public static class PropertyFinder<T> where T : ISQLiteRow
    {
        /* Her benyttes der en Delegate Expression. Dette bliver gjort for, at gøre Func<T, TProperty> muligt.
         * Func<T, TPropery> betyder den benytter T (ISQLiteRow) til, at finde en given property (PropertyInfo).
         * Herefter returner Func<T, TProperty> den fundet property, som typen TProperty, hvilket er
         * typen den fundet property er.
         */
        public static PropertyInfo Find<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            /* Siden expression er en property kan man cast MemberExpression på den for, 
             * at få propertien af lambda udtrykket. F.eks. Bob.Name vil være: x => x.Name.
             * Herefter bliver det konverteret til en PropertyInfo, siden MemberExpression får en property ud.
             * Herefter benyttes Member til, at hive fat i selve propertien.
             */
            return (PropertyInfo)((MemberExpression)expression.Body).Member;
        }
    }
}
