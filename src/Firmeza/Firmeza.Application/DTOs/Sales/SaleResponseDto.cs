namespace Firmeza.Application.DTOs.Sales;

public class SaleResponseDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Guid ClientId { get; set; }
    public decimal Total { get; set; }
    public bool IsActive { get; set; }
}
