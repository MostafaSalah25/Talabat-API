using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Talabat.BLL.Specifications
{
    public interface ISpecification<T> 
    {
        // Signatures for Props 
        public Expression<Func<T, bool >> Criteria { get; set; }
         
        public List<Expression<Func<T, object>>> Includes { get; set; } 

        // Sorting 
        public Expression<Func<T, object>> OrderBy { get; set; } 
        public Expression<Func<T, object>> OrderByDesc { get; set; } 

        // Pagination 
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; } 

    }
}
