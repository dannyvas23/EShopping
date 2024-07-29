using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.CatalogsContextSeed;

public class TypeContextSeed
{
    public static void SeedData(IMongoCollection<ProductType> typeCollection)
    {
        bool checkTypes = typeCollection.Find(b => true).Any();
        // path = Path.Combine("Data", "SeedData", "types.json");
        if (!checkTypes)
        {
           //var typesData = File.ReadAllText("Services/Catalog/Catalog.Infrastructure/Data/SeedData/types.json");
           
           var basePath = AppDomain.CurrentDomain.BaseDirectory;
           var filePath = Path.Combine(basePath, "Services","Catalog", "Catalog.Infrastructure", "Data", "SeedData", "types.json");
           var typesData = File.ReadAllText(filePath);
           
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            if (types != null)
            {
                foreach (var item in types)
                {
                    typeCollection.InsertOneAsync(item);
                }
            }
        }
    }
}