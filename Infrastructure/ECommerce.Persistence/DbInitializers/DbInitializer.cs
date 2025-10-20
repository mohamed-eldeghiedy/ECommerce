using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Products;
using ECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Persistence.DbInitializers
{
    internal class DbInitializer(ApplicationDbContext dbContext ) : IDbInitializer
    {
        public async Task InitializerAsync()
        {
           if((await dbContext.Database.GetPendingMigrationsAsync()).Any())
                await dbContext.Database.MigrateAsync();
           if(!dbContext.ProductTypes.Any())
           { 
             var TypesData = await File.ReadAllTextAsync(@"..\Infrastructure\ECommerce.Persistence\Context\DataSeed\types.json");
             var types =JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if(types != null && types.Any())
                {
                     dbContext.ProductTypes.AddRange(types);
                    
                }
                await dbContext.SaveChangesAsync();
           }


          
            if (!dbContext.ProductBrands.Any())
            {
                var BrandsData = await File.ReadAllTextAsync(@"..\Infrastructure\ECommerce.Persistence\Context\DataSeed\brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if (Brands != null && Brands.Any())
                {
                    dbContext.ProductBrands.AddRange(Brands);

                }
                await dbContext.SaveChangesAsync();
            }


            if (!dbContext.Products.Any())
            {
                var ProductsData = await File.ReadAllTextAsync(@"..\Infrastructure\ECommerce.Persistence\Context\DataSeed\products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (Products != null && Products.Any())
                {
                    dbContext.Products.AddRange(Products);

                }
                await dbContext.SaveChangesAsync();
            }


        }
    }
}
