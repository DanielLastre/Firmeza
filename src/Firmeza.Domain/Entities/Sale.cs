using Firmeza.Domain.Common;
using Firmeza.Domain.ValueObjects;

namespace Firmeza.Domain.Entities;

public class Sale : Entity
{
    public DateTime Date { get; private set; }

    public Guid ClientId { get; private set; }
    public Client Client { get; private set; }

    public Money Total { get; private set; }

    public ICollection<SaleDetail> SaleDetails { get; private set; } = new List<SaleDetail>();

    private Sale()
    {
    }

    public Sale(
        DateTime date,
        Guid clientId,
        decimal total)
    {
        if (date > DateTime.Now)
            throw new ArgumentException(
                "La fecha de la venta no puede ser futura.");

        if (clientId == Guid.Empty)
            throw new ArgumentException(
                "El cliente es obligatorio.");

        Date = date;
        ClientId = clientId;
        Total = Money.Create(total);
    }
}
