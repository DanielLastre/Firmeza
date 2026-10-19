namespace Firmeza.Domain.ValueObjects;

public sealed record TaxId(string Value)
{
    public static TaxId Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(
                "El identificador fiscal (NIT/TaxId) es obligatorio.");

        var trimmed = value.Trim();
        if (trimmed.Length < 8 || trimmed.Length > 50)
            throw new ArgumentException(
                "El identificador fiscal no es válido (debe tener entre 8 y 50 caracteres).");

        return new TaxId(trimmed);
    }
}
