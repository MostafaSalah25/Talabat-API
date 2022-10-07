
using Talabat.DAL.Entities;

namespace Talabat.BLL.Specifications.ProductSpecifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams) // filteration
             : base(P =>  
              (!productParams.BrandId.HasValue || P.ProductBrandId == productParams.BrandId.Value) && 
              (!productParams.TypeId.HasValue || P.ProductTypeId == productParams.TypeId.Value) &&
             // search
             (string.IsNullOrEmpty(productParams.Search) || (P.Name.ToLower().Contains(productParams.Search))) 
         )
        {
        }
    }
}
