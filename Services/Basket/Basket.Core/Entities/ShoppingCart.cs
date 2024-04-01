namespace Basket.Core.Entities;

public class ShoppingCart
{
    public string UserName { get; set; }
    public List<ShoppingCarItem> Items { get; set; } = new List<ShoppingCarItem>();

    public ShoppingCart()
    {
            
    }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
    
}