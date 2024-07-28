﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastructure.Extensions;

public static class DbExtension
{
    public static IHost MigrateDatabase<TContext>(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var config = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();
            try
            {
                Console.WriteLine("------------------->>>>>>>>>>>MIGRACION<<<<<<<<<<<<<<<<<<<------------------------");
                logger.LogInformation("Discount DB Migration Started");
                ApplyMigrations(config);
                logger.LogInformation("Discount DB Migration Completed");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        return host;
    }

    private static void ApplyMigrations(IConfiguration config)
    {
        string valor = "";// "Server=discountdb;Port=5432;Database=DiscountDb;User Id=admin;Password=Password@1";
        valor = config.GetValue<string>("DatabaseSettings:ConnectionString");
        Console.WriteLine("------------------->>>>>>>>>>>${valor}<<<<<<<<<<<<<<<<<<<------------------------");
        Console.WriteLine(valor);
        Console.WriteLine("------------------->>>>>>>>>>>${valor}<<<<<<<<<<<<<<<<<<<------------------------");
        
        try
        {
            using var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));
        
            connection.Open();
            using var cmd = new NpgsqlCommand()
            {
                Connection = connection
            };
            //cmd.Connection = connection;
            cmd.CommandText = "DROP TABLE IF EXISTS Coupon";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                ProductName VARCHAR(500) NOT NULL,
                                                Description TEXT,
                                                Amount INT)";
            cmd.ExecuteNonQuery();
        
            cmd.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Adidas Quick Force Indoor Badminton Shoes', 'Shoe Discount', 500);";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Yonex VCORE Pro 100 A Tennis Racquet (270gm, Strung)', 'Racquet Discount', 700);";
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"error  while connecting to the database: {ex.Message}");
        }
        
        
    }
}