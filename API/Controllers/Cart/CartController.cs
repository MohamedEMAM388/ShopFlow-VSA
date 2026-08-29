using API.Features.AddToCart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Cart;

public class CartController : Controller
{
    private readonly IMediator _mediator;


    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    public async Task<IActionResult> AddToCart(AddToCartCommand cartCommand)
    {
       var result = await  _mediator.Send(cartCommand);
       return Ok(result);
    }
}