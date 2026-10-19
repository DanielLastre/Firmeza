namespace Firmeza.Application.DTOs.Sales;

public class SaleDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Guid ClientId { get; set; }
    public decimal Total { get; set; }
}
