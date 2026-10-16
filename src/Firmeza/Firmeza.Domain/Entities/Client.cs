using Firmeza.Domain.Common;

namespace Firmeza.Domain.Entities;

public class Client : Entity
{
    public string Name { get; private set; } = string.Empty;
    public int Age { get; private set; }
    public string Phone { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Document { get; private set; } = string.Empty;
    public Guid EnterpriseId { get; private set; }

    protected Client() { }

    public Client(string name, int age, string phone, string email, string document, Guid enterpriseId)
    {
        Validate(name, age, phone, email, document, enterpriseId);

        Name = name.Trim();
        Age = age;
        Phone = phone.Trim();
        Email = email.Trim();
        Document = document.Trim();
        EnterpriseId = enterpriseId;
    }

    public void Update(string name, int age, string phone, string email, string document, Guid enterpriseId)
    {
        Validate(name, age, phone, email, document, enterpriseId);

        Name = name.Trim();
        Age = age;
        Phone = phone.Trim();
        Email = email.Trim();
        Document = document.Trim();
        EnterpriseId = enterpriseId;
    }

    private static void Validate(string name, int age, string phone, string email, string document, Guid enterpriseId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre del cliente es obligatorio.");

        if (age < 0 || age > 150)
            throw new ArgumentException("La edad debe estar entre 0 y 150 años.");

        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("El teléfono es obligatorio.");

        var trimmedPhone = phone.Trim();
        if (trimmedPhone.Length < 7 || trimmedPhone.Length > 50)
            throw new ArgumentException("El teléfono debe tener entre 7 y 50 caracteres.");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@') || !email.Contains('.'))
            throw new ArgumentException("El formato del correo electrónico no es válido.");

        if (string.IsNullOrWhiteSpace(document))
            throw new ArgumentException("El documento de identidad es obligatorio.");

        var trimmedDoc = document.Trim();
        if (trimmedDoc.Length < 5 || trimmedDoc.Length > 50)
            throw new ArgumentException("El documento debe tener entre 5 y 50 caracteres.");

        if (enterpriseId == Guid.Empty)
            throw new ArgumentException("La empresa asociada es obligatoria.");
    }
}
