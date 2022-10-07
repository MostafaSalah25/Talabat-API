using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.DAL.Entities;
using Talabat.DAL.Entities.Order_Aggregate;

namespace Talabat.DAL
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory) 
        {
            try
            {   
                if (!context.ProductTypes.Any()) // just add first time when no ProdT in Database >  
                {
                    var typesData = File.ReadAllText("../Talabat.DAL/Data/SeedData/types.json"); 
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach (var type in types)
                        context.ProductTypes.Add(type);
                    await context.SaveChangesAsync();
                }
                if (!context.ProductBrands.Any())  // sec file of data 
                {
                    var brandsData = File.ReadAllText("../Talabat.DAL/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData); 
                    foreach (var brand in brands)
                        context.ProductBrands.Add(brand);
                    await context.SaveChangesAsync();
                }
                if (!context.Products.Any())  // third..
                {
                    var productsData = File.ReadAllText("../Talabat.DAL/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData); 
                    foreach (var product in products)
                        context.Products.Add(product);
                    await context.SaveChangesAsync();
                }
                //related to Order > no have Json file now
                if (!context.DeliveryMethods.Any())
                {
                    var DeliveryMethodsData = File.ReadAllText("../Talabat.DAL/Data/SeedData/delivery.json");
                    var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);
                    foreach (var DeliveryMethod in DeliveryMethods)
                        context.Set<DeliveryMethod>().Add(DeliveryMethod);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
