using System;


namespace Talabat.BLL.Specifications.ProductSpecifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        // sorting
        public string Sort { get; set; } 

        // filteration 
        public int? BrandId { get; set; } 
        public int? TypeId { get; set; }

        // Pagination
        public int PageIndex { get; set; } = 1; 
        private int pageSize = 5; 
        public int PageSize   
        {
            get { return pageSize ; }
            set { pageSize = value > MaxPageSize ? 50 : value; }  
        }
        // Search 
        private string search;
        public string Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }
    }
}
