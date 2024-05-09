using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Models.Requests;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BuildingBlocks.Infrastructure.Extensions;

public static class IQueryableExtensions
{
    /// <summary>
    /// apply where clause, if specified
    /// </summary>
    /// <typeparam name="T">generic type specified at caller</typeparam>
    /// <param name="query">source query being modified</param>
    /// <param name="filter">where clause details</param>
    /// <returns>modified query</returns>
    public static IQueryable<T> ExtendWhere<T>(this IQueryable<T> query, Expression<Func<T, bool>>? filter)
        => filter == null ? query : query.Where(filter);
    /// <summary>
    /// apply incldues list to source query
    /// </summary>
    /// <typeparam name="T">generic type specified at caller</typeparam>
    /// <param name="query">source query being modified</param>
    /// <param name="includeFunc">query modifier function</param>
    /// <returns>modified query</returns>
    public static IQueryable<T> ExtendIncludes<T>(this IQueryable<T> query, Func<IQueryable<T>, IQueryable<T>>? includeFunc = null)
        => includeFunc == null ? query : includeFunc(query);
    /// <summary>
    /// apply pagination model, if specified
    /// </summary>
    /// <typeparam name="T">generic type specified at caller</typeparam>
    /// <param name="query">source query being modified</param>
    /// <param name="model">pagination details</param>
    /// <returns>modified query</returns>
    public static IQueryable<T> ExtendPagination<T>(this IQueryable<T> query, PaginationModel? model)
        => model == null ? query : query.Skip((model.Page - 1) * model.Page).Take(model.PageSize);
    /// <summary>
    /// apply order by clause asc|desc
    /// </summary>
    /// <typeparam name="T">generic type specified at caller</typeparam>
    /// <param name="query">source query being modified</param>
    /// <param name="orderByExpression">order by statement</param>
    /// <param name="ascending">order by direction</param>
    /// <returns>modified query</returns>
    public static IQueryable<T> ExtendOrderBy<T>(this IQueryable<T> query, Expression<Func<T, object>>? orderBy, bool ascending = true)
    {
        if (ascending)
            return orderBy == null ? query : (query.OrderBy(orderBy));          
       return orderBy == null ? query : (query.OrderByDescending(orderBy));
    }

    /// <summary>
    /// build order-by clause from string specifier
    /// </summary>
    /// <typeparam name="T">generic type specified at caller</typeparam>
    /// <param name="query">source query being modified</param>
    /// <param name="sortString">comma-separated sorting instructions in the format: {field}|{optional: asc/desc}, ...etc! defaulting to asc if sort-direction not specified</param>
    /// <returns>modified query</returns>
    public static IQueryable<T> ExtendOrderBy<T>(this IQueryable<T> query, string sortString)
    {
        if(string.IsNullOrEmpty(sortString))
            return query;

        //  parse sorting instruction
        foreach (var sortInstruction in sortString.Split(','))
        {
            var parts = sortInstruction.Split('|');
            string sort_field = parts[0].Trim(),
                sort_dir = parts.Length > 1 ? parts[1] : "asc";

            var parameter = Expression.Parameter(typeof(T), "e");
            var property = Expression.PropertyOrField(parameter, sort_field);
            var conversion = Expression.Convert(property, typeof(object));
            var lambda = Expression.Lambda<Func<T, object>>(conversion, parameter);

            //  if we have an existing sort expression, apply then-by...
            if (query.Expression.Type == typeof(IOrderedQueryable<T>))
            {
                query = (sort_dir == "asc") ?
                        Queryable.ThenBy((IOrderedQueryable<T>)query, lambda) :
                        Queryable.ThenByDescending((IOrderedQueryable<T>)query, lambda);
            }
            else
            {
                query = (sort_dir == "asc") ?
                        Queryable.OrderBy(query, lambda) :
                        Queryable.OrderByDescending(query, lambda);
            }
        }

        //  return modified query
        return query;
    }

    /// <summary>
    /// apply includes list to source query
    /// </summary>
    /// <param name="query">source query being modified</param>
    /// <returns>modified query</returns>
    public static IQueryable<Customer> ExtendCompanyIncludes(this IQueryable<Customer> query)
    {
        return query.Include(c => c.Orders);
    }

    /// <summary>
    /// apply includes list to source query
    /// </summary>
    /// <param name="query">source query being modified</param>
    /// <returns>modified query</returns>
    public static IQueryable<Vehicle> ExtendVehicleIncludes(this IQueryable<Vehicle> query)
    {
        return query.Include(c => c.VehicleComponents)
                    .ThenInclude(c => c.Component);
    }

    /// <summary>
    /// apply includes list to source query
    /// </summary>
    /// <param name="query">source query being modified</param>
    /// <returns>modified query</returns>
    public static IQueryable<Order> ExtendOrderIncludes(this IQueryable<Order> query)
    {
        return query.Include(c => c.OrderItems);
    }


}
