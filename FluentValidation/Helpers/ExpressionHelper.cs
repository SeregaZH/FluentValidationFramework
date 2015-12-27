using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentValidation.Helpers
{
    internal static class ExpressionHelper
    {
        internal static string ResolvePropertyName<TModel, TValue>(this Expression<Func<TModel, TValue>> propertyExpression)
        {
            var memberExpr = propertyExpression.Body as MemberExpression;

            var propertyInfo = memberExpr?.Member as PropertyInfo;

            if (propertyInfo != null)
            {
                return propertyInfo.Name;
            }

            throw new ArgumentException("Expression is not refered to property");
        }
    }
}
