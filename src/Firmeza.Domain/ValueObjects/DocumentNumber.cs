namespace Firmeza.Domain.ValueObjects;

public sealed record DocumentNumber(string Value)
{
    public static DocumentNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(
                "El documento es obligatorio.");

        var trimmed = value.Trim();
        if (trimmed.Length < 5 || trimmed.Length > 50)
            throw new ArgumentException(
                "El documento no es válido (debe tener entre 5 y 50 caracteres).");

        return new DocumentNumber(trimmed);
    }
}
