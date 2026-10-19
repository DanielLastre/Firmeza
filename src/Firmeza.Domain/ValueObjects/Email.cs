namespace Firmeza.Domain.ValueObjects;

public sealed record Email(string Value)
{
    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(
                "El email es obligatorio.");

        var trimmed = value.Trim();
        if (!trimmed.Contains('@') || !trimmed.Contains('.'))
            throw new ArgumentException(
                "El email no tiene un formato válido.");

        return new Email(trimmed);
    }
}
