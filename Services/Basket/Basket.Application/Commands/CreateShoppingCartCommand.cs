using Basket.Application.Responses;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Commands;

public class CreateShoppingCartCommand: IRequest<ShoppingCartResponse>
{
    public string UserName { get; set; }
    public List<ShoppingCarItem> Items { get; set; }

    public CreateShoppingCartCommand(string userName, List<ShoppingCarItem> items)
    {
        UserName = userName;
        Items = items;
    }
}