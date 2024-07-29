using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.CatalogsContextSeed;

public static class BrandContextSeed
{
    public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
    {
        bool checkBrands = brandCollection.Find(b => true).Any();
        //string path = Path.Combine("Data", "SeedData", "brands.json");
        if (!checkBrands)
        {
            //var brandsData = File.ReadAllText("/Services/Catalog/Catalog.Infrastructure/Data/SeedData/brands.json");
                        
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(basePath, "Services","Catalog", "Catalog.Infrastructure", "Data", "SeedData", "brands.json");
            var brandsData = File.ReadAllText(filePath);
            
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            if (brands != null)
            {
                foreach (var item in brands)
                {
                    brandCollection.InsertOneAsync(item);
                }
            }
        }
    } 
}