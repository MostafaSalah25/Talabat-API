using Talabat.BLL.Specifications.ProductSpecifications;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        //  const used when need to get all products 
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)

            : base(  P =>
               (!productParams.BrandId.HasValue || P.ProductBrandId == productParams.BrandId.Value) && 
              
               (!productParams.TypeId.HasValue || P.ProductTypeId == productParams.TypeId.Value) &&
               // search
               (string.IsNullOrEmpty(productParams.Search) || (P.Name.ToLower().Contains(productParams.Search))) 
                 )

        {   // return Nav props > ProdType , ProdBrand
            AddInclude(P=>P.ProductType);  
            AddInclude(P=>P.ProductBrand);

            // Pagination ... if send pageSize = 5 , pageIndex = 2  so want skip first 5 & take next five > 
            ApplyPagination(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            // sorting 
            if( !string.IsNullOrEmpty(productParams.Sort))  
            {
                switch(productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);   
                        break;
                }
            }
            else  // default sorted by name   
            {
                AddOrderBy(P => P.Name);
            }


        }

        // this const used when need to get a specific product with id
        public ProductsWithTypesAndBrandsSpecification(int id):base( P=>P.Id ==id ) // chain to constructor take Lam Exp
        {
            AddInclude(P => P.ProductType);
            AddInclude(P => P.ProductBrand); 
        }
    }
}
