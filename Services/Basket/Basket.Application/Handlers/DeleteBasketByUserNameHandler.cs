using Basket.Application.Commands;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers;

public class DeleteBasketByUserNameHandler: IRequestHandler<DeleteBasketByUserNameCommand, bool>
{
    private readonly IBasketRepository _basketRepository;

    public DeleteBasketByUserNameHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<bool> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
    {
        return await _basketRepository.DeleteBasket(request.UserName);
        
    }
}