using System.Text.Json;
using API.Data;
using API.Features.AddToCart.DTOS;
using API.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.AddToCart;

public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand , ResponseDto>
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AddToCartCommandValidator _commandValidator;
    private const string CartSessionKey = "Cart";

    public AddToCartCommandHandler(AppDbContext context , 
                     IHttpContextAccessor httpContextAccessor , AddToCartCommandValidator commandValidator)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _commandValidator = commandValidator;
    }

    public async Task<ResponseDto> Handle(AddToCartCommand cartCommand ,
                                            CancellationToken cancellationToken)
    {
        // validator
        var validationResult = await _commandValidator.ValidateAsync(cartCommand);
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        // get product
        var product = await _context.Set<Product>().
                                    FirstOrDefaultAsync(x => x.Id == cartCommand.Id , cancellationToken);
        if (product is null)
            throw new Exception("Product not found");
        
        // get cart from session
        var session = _httpContextAccessor.HttpContext!.Session;
        var cartJson = session.GetString(CartSessionKey);
        var cart = string.IsNullOrEmpty(cartJson)
            ? [] : JsonSerializer.Deserialize<List<CartItemDto>>(cartJson) 
                   ?? [];
        
        // add product to cart
        var existingItem = cart.FirstOrDefault(x => x.ProductId == cartCommand.Id);

        if (existingItem is not null)
        {
            existingItem.Quantity += cartCommand.Quantity;
            
        }
        else
        {
            cart.Add(new CartItemDto
            {
                ProductId =  cartCommand.Id,
                Quantity = cartCommand.Quantity,
                Price = product.Price
                
            });
        }

        // save cart to session
        var newCartJson = JsonSerializer.Serialize(cart);
        session.SetString(CartSessionKey, newCartJson);

        return new ResponseDto
        {

            ProductId = product.Id,
            Quantity = cartCommand.Quantity,
            Message = "Success"

        };
        


    }


}