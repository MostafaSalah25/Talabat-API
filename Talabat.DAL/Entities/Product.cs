using System;

namespace Talabat.DAL.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string  PictureUrl { get; set; } 
        public ProductType ProductType { get; set; } // Navigational prop > relation > one ProductType have many Products
        // work Eager Loading not Lazy Loading

        public int ProductTypeId { get; set; }  
        public ProductBrand ProductBrand { get; set; } // Navigational prop ..
        public int ProductBrandId { get; set; } 
    }
}
