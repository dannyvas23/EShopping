using Basket.Core.Entities;

namespace Basket.Application.Responses;

public class ShoppingCartResponse
{
    public string UserName { get; set; }
    public List<ShoppingCarItem> Items { get; set; } 

   /* public ShoppingCartResponse()
    {
        
    }*/

    public ShoppingCartResponse(string userName, List<ShoppingCarItem> items)
    {
        UserName = userName;
        Items = items;
    }

    public decimal TotalPrice()
    {
        decimal totalPrice = 0;

        foreach (var item in Items)
        {
            totalPrice += item.Price * item.Quantity;
        }

        return totalPrice;
    }
    
}