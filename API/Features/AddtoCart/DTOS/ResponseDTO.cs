namespace API.Features.AddToCart.DTOS;

public class ResponseDto
{
    public int ProductId { get; set; } 

    public int Quantity { get; set; }

    public string Message { get; set; } = null!;
}