using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data
{
    class SpecificationEvaluator<T> where T:class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;
            if (specification.Criteria != null)
            {
                query.Where(specification.Criteria);
            }

            query = specification.Includes.Aggregate(query,
                (current, include) => current.Include(include));

            if(specification.OrderBy != null)
            {
                query.OrderBy(specification.OrderBy);
            }

            if (specification.IsPagingEnabled)
            {
                query.Skip(specification.Skip)
                    .Take(specification.Take);
            }
            return query;
        }
    }
}
