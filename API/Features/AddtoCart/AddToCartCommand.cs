using API.Features.AddToCart.DTOS;
using MediatR;

namespace API.Features.AddToCart;

public record AddToCartCommand(int Id , int Quantity  ) : IRequest<ResponseDto>;
