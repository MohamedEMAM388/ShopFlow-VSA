using API.Features.AddToCart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Cart;

[ApiController]
[Route("api/[controller]")]
public class CartController : Controller
{
    private readonly IMediator _mediator;


    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddToCart(AddToCartCommand cartCommand)
    {
       var result = await  _mediator.Send(cartCommand);
       return Ok(result);
    }
}