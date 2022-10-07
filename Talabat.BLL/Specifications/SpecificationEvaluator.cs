using Microsoft.EntityFrameworkCore;
using System.Linq;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Specifications
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity 
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery , ISpecification<TEntity> spec )
        { 
            var query = inputQuery; 
            if (spec.Criteria !=null) 
                query = query.Where(spec.Criteria);

            // Sorting 
            if (spec.OrderBy != null) 
                query = query.OrderBy(spec.OrderBy); 
            if (spec.OrderByDesc != null)
                query = query.OrderByDescending(spec.OrderByDesc);

            // Pagination 
            if (spec.IsPaginationEnabled )
                query = query.Skip(spec.Skip).Take(spec.Take);
            // aggregate prop List of Includes y have in GetProd or GetProds
            query = spec.Includes.Aggregate(query ,(currentQuery , include) =>currentQuery.Include(include)); 
        
            return query;
        }
    }
}
