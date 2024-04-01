﻿using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infraestructure.Repositories;

public class BasketRepository: IBasketRepository
{
    private readonly IDistributedCache _redisCache;

    public BasketRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }
    
    public async Task<ShoppingCart> GetBasket(string userName)
    {
        var basket = await _redisCache.GetStringAsync(userName);
        if (string.IsNullOrEmpty(basket))
        {
            return null;
        }

        return JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
    {
        await _redisCache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart));
        return await GetBasket(shoppingCart.UserName);
    }

    public async Task<bool>  DeleteBasket(string userName)
    {
        try
        {
            await _redisCache.RemoveAsync(userName);
            return true; // Si la eliminación fue exitosa, devuelve true
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar objeto basket: {ex.Message}");
            return false; 
        }
    }
}