using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.CatalogsContextSeed;

public class ProductContextSeed
{
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        bool checkProducts = productCollection.Find(b => true).Any();
        //string path = Path.Combine("Data", "SeedData", "products.json");
        if (!checkProducts)
        {
            //var productsData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/products.json");
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(basePath, "Services","Catalog", "Catalog.Infrastructure", "Data", "SeedData", "products.json");
            var productsData = File.ReadAllText(filePath);
            
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products != null)
            {
                foreach (var item in products)
                {
                    productCollection.InsertOneAsync(item);
                }
            }
        }
    }
}