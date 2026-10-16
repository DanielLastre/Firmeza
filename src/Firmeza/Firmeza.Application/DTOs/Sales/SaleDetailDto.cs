namespace Firmeza.Application.DTOs.Sales;

public class SaleDetailDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Subtotal { get; set; }
    public Guid SaleId { get; set; }
}
