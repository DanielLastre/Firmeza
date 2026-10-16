using Firmeza.Domain.Common;
using Firmeza.Domain.ValueObjects;

namespace Firmeza.Domain.Entities;

public class SaleDetail : Entity
{

    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }

    public int Quantity { get; private set; }

    public Money Price { get; private set; }
    public Money Subtotal { get; private set; }

    public Guid SaleId { get; private set; }
    public Sale Sale { get; private set; }

    private SaleDetail()
    {
    }

    public SaleDetail(
        Guid productId,
        int quantity,
        decimal price,
        Guid saleId)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException(
                "El producto es obligatorio.");

        if (quantity <= 0)
            throw new ArgumentException(
                "La cantidad debe ser mayor que cero.");

        if (saleId == Guid.Empty)
            throw new ArgumentException(
                "La venta es obligatoria.");

        ProductId = productId;
        Quantity = quantity;
        Price = Money.Create(price);
        SaleId = saleId;

        Subtotal = Money.Create(quantity * price);
    }
}
