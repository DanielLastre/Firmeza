using Firmeza.Domain.Common;
using Firmeza.Domain.ValueObjects;

namespace Firmeza.Domain.Entities;

public class Product : Entity
{
    public string Name { get; private set; }
    public string Brand { get; private set; }
    public Money Price { get; private set; }
    public string Description { get; private set; }
    public int Stock { get; private set; }

    private Product()
    {
    }

    public Product(
        string name,
        string brand,
        decimal price,
        string description,
        int stock)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(brand))
            throw new ArgumentException("La marca es obligatoria.");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("La descripción es obligatoria.");

        if (stock < 0)
            throw new ArgumentException(
                "El stock no puede ser negativo.");

        Name = name;
        Brand = brand;
        Price = Money.Create(price);
        Description = description;
        Stock = stock;
    }
}
