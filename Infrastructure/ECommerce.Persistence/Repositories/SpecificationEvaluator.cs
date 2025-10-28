using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Repositories
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> ApplySpecification<TEntity>
            ( this IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> specification)
            where TEntity : class
        {
            var query = inputQuery;  

            if (specification.Criteria is not null)            
                query = query.Where(specification.Criteria);
            
            query = specification.Includes
                .Aggregate(query,
                (query, include) => query.Include(include));

            if (specification.OrderBy is not null)
                query = query.OrderBy(specification.OrderBy);
            else if (specification.OrderByDescending is not null)
                query = query.OrderByDescending(specification.OrderByDescending);

            if (specification.IsPagingEnabled)
                query = query.Skip(specification.Skip)
                    .Take(specification.Take);

            return query;
        }
    }
}
