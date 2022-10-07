using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Talabat.BLL.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> 
    {
        // implement Properties 
        public Expression<Func<T, bool>> Criteria { get; set; } 
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        // sorting 
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        // Pagination
        public int Take { get ; set ; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get ; set ; }


        // const used to get a specific Entity as product with id 
        public BaseSpecification(Expression<Func<T, bool>> Criteria) 
        {
            this.Criteria= Criteria; 
        }

        // next meths just setter > set value to Props as Includes  
        public void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }
        // Sorting
        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }
        public void AddOrderByDescending(Expression<Func<T, object>> orderByDescending)
        {
            OrderByDesc = orderByDescending;
        }

        // Pagination
        public void ApplyPagination( int skip ,int take )
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }

    }
}
