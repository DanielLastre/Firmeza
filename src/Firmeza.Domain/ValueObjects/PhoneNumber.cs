namespace Firmeza.Domain.ValueObjects;

public sealed record PhoneNumber(string Value)
{
    public static PhoneNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(
                "El teléfono es obligatorio.");

        var trimmed = value.Trim();
        if (trimmed.Length < 7 || trimmed.Length > 50)
            throw new ArgumentException(
                "El teléfono no es válido (debe tener entre 7 y 50 caracteres).");

        return new PhoneNumber(trimmed);
    }
}
