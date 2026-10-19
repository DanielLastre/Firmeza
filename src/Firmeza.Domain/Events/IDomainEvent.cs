namespace Firmeza.Domain.Events;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
