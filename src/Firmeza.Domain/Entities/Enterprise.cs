using Firmeza.Domain.Common;
using Firmeza.Domain.Enums;
using Firmeza.Domain.ValueObjects;

namespace Firmeza.Domain.Entities;

public class Enterprise : Entity
{
    public string Name { get; private set; }
    public string TradeName { get; private set; }
    public EnterpriseType Type { get; private set; }
    public string Address { get; private set; }

    public PhoneNumber Phone { get; private set; }
    public Email Email { get; private set; }
    public TaxId TaxId { get; private set; }

    private Enterprise()
    {
    }

    public Enterprise(
        string name,
        string tradeName,
        EnterpriseType type,
        string address,
        string phoneNumber,
        string email,
        string taxId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre de la empresa es obligatorio.");

        if (string.IsNullOrWhiteSpace(tradeName))
            throw new ArgumentException("El nombre comercial es obligatorio.");

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("La dirección es obligatoria.");

        Name = name;
        TradeName = tradeName;
        Type = type;
        Address = address;

        Phone = PhoneNumber.Create(phoneNumber);
        Email = Email.Create(email);
        TaxId = TaxId.Create(taxId);
    }

    public void Update(
        string name,
        string tradeName,
        EnterpriseType type,
        string address,
        string phoneNumber,
        string email,
        string taxId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre de la empresa es obligatorio.");

        if (string.IsNullOrWhiteSpace(tradeName))
            throw new ArgumentException("El nombre comercial es obligatorio.");

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("La dirección es obligatoria.");

        Name = name;
        TradeName = tradeName;
        Type = type;
        Address = address;

        Phone = PhoneNumber.Create(phoneNumber);
        Email = Email.Create(email);
        TaxId = TaxId.Create(taxId);
    }
}
