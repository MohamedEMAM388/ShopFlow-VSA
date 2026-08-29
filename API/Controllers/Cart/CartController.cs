using API.Features.AddToCart;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Cart;

public class CartController : Controller
{
    private readonly AddToCartCommandHandler _handler;

    public CartController(AddToCartCommandHandler handler)
    {
        _handler = handler;
    }
    

    public async Task<IActionResult> AddToCart(AddToCartCommand cartCommand)
    {
       var result = await  _handler.Handle( cartCommand);
       return Ok(result);
    }
}