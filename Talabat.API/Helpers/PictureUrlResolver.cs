using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.API.Dtos;
using Talabat.DAL.Entities;

namespace Talabat.API.Helpers
{
    public class PictureUrlResolver :IValueResolver<Product,ProductToReturnDto,string>  
    {
    
        private readonly IConfiguration configuration;
        public PictureUrlResolver(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(destMember))
                return $"{configuration["ApiUrl"]}{source.PictureUrl}"; //https://localhost:5001/wwwroot/Images/Products/sb-ang1.png
            return null;

        }
    }
}
