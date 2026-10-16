namespace Firmeza.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public bool IsActive { get; private set; } = true;

    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
}
