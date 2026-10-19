namespace Firmeza.Domain.ValueObjects;

public sealed record Money(decimal Amount)
{
    public static Money Create(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException(
                "El valor monetario no puede ser negativo.");

        return new Money(amount);
    }
}
