using System.Linq.Expressions;

namespace WebApplication4.Extinsion
{
    public static class QuerySortExtension
    {


        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, string sortField, string sortOrder)
        {
            if (string.IsNullOrEmpty(sortField))
                return query;

            var parameter = Expression.Parameter(typeof(T), "x");

            var property = Expression.PropertyOrField(parameter, sortField);

            var lambda = Expression.Lambda(property, parameter);

            string method = sortOrder == "desc"
                ? "OrderByDescending"
                : "OrderBy";

            var resultExpression = Expression.Call(
                typeof(Queryable),
                method,
                new Type[] { typeof(T), property.Type },
                query.Expression,
                Expression.Quote(lambda));

            return query.Provider.CreateQuery<T>(resultExpression);
        }
    }


}
