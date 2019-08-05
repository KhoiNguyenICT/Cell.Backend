using Cell.Common.Specifications;
using Cell.Core.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Cell.Common.Linq
{
    public static class LinqExtensions
    {
        public static IQueryable<T> SortBy<T>(this IQueryable<T> source, params string[] sortExpression) where T : class
        {
            if (sortExpression == null || sortExpression.Length == 0)
                return source;

            IOrderedQueryable<T> orderedQuery = null;
            for (var index = 0; index < sortExpression.Length; index++)
            {
                var exp = sortExpression[index];
                if (string.IsNullOrEmpty(exp))
                {
                    continue;
                }
                var sortField = Regex.Replace(sortExpression[index], @"[\+\-]", string.Empty);
                if (sortExpression[index].StartsWith("-"))
                {
                    orderedQuery = index == 0 ? source.OrderByDescending(sortField) : orderedQuery.ThenByDescending(sortField);
                }
                else
                {
                    orderedQuery = index == 0 ? source.OrderBy(sortField) : orderedQuery.ThenBy(sortField);
                }
            }
            return orderedQuery ?? source;
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string fieldName) where T : class
        {
            var resultExp = GenerateMethodCall(source, "OrderBy", fieldName);
            return source.Provider.CreateQuery<T>(resultExp) as IOrderedQueryable<T>;
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string fieldName) where T : class
        {
            var resultExp = GenerateMethodCall(source, "OrderByDescending", fieldName);
            return source.Provider.CreateQuery<T>(resultExp) as IOrderedQueryable<T>;
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string fieldName) where T : class
        {
            var resultExp = GenerateMethodCall(source, "ThenBy", fieldName);
            return source.Provider.CreateQuery<T>(resultExp) as IOrderedQueryable<T>;
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string fieldName) where T : class
        {
            var resultExp = GenerateMethodCall(source, "ThenByDescending", fieldName);
            return source.Provider.CreateQuery<T>(resultExp) as IOrderedQueryable<T>;
        }

        public static IEnumerable<T> SortBy<T>(this IEnumerable<T> source, params string[] sortExpression) where T : class
        {
            if (sortExpression == null || sortExpression.Length == 0)
                return source;

            for (var index = 0; index < sortExpression.Length; index++)
            {
                var exp = sortExpression[index];
                if (string.IsNullOrEmpty(exp))
                {
                    continue;
                }
                var sortField = Regex.Replace(sortExpression[index], @"[\+\-]", string.Empty);
                PropertyDescriptor sortProperty = TypeDescriptor.GetProperties(typeof(T)).Find(sortField, true);
                if (sortExpression[index].StartsWith("-"))
                {
                    source = source.OrderByDescending(a => sortProperty.GetValue(a)).ToList();
                }
                else
                {
                    source = source.OrderBy(a => sortProperty.GetValue(a)).ToList();
                }
            }
            return source;
        }

        private static MethodCallExpression GenerateMethodCall<T>(IQueryable<T> source, string methodName, string fieldName) where T : class
        {
            var type = typeof(T);
            var selector = GenerateSelector<T>(fieldName, out var selectorResultType);
            var resultExp = Expression.Call(typeof(Queryable), methodName,
                new[] { type, selectorResultType },
                source.Expression, Expression.Quote(selector));
            return resultExp;
        }

        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            if (second == null) return first;
            return first.Compose(second, Expression.OrElse);
        }

        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            if (second == null) return first;
            return first.Compose(second, Expression.AndAlso);
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> predicate)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.Not(predicate.Body), predicate.Parameters[0]);
        }

        public static Expression<Func<T, bool>> Compose<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second,
            Func<Expression, Expression, Expression> merge)
        {
            if (first.IsEquals(t => true)) return second;
            if (second.IsEquals(t => true)) return first;
            return ComposeExpression(first, second, merge);
        }

        public static Expression<T> ComposeExpression<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            if (first.IsEquals(second)) return first;
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Specification<T> ToSpec<T>(this Expression<Func<T, bool>> expression)
        {
            return new Specification<T>(expression);
        }

        private static LambdaExpression GenerateSelector<T>(string propertyName, out Type resultType) where T : class
        {
            // Create a parameter to pass into the Lambda expression (Entity => Entity.OrderByField).
            var parameter = Expression.Parameter(typeof(T), "Entity");
            //  create the selector part, but support child properties
            PropertyInfo property;
            Expression propertyAccess;
            if (propertyName.Contains('.'))
            {
                // support to be sorted on child fields.
                var childProperties = propertyName.Split('.');
                property = typeof(T).GetProperty(childProperties[0], BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
                for (var i = 1; i < childProperties.Length; i++)
                {
                    property = property.PropertyType.GetProperty(childProperties[i], BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                    propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                }
            }
            else
            {
                property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
            }
            resultType = property.PropertyType;
            // Create the order by expression.
            return Expression.Lambda(propertyAccess, parameter);
        }
    }
}