using Sirus.Application.Common;

namespace Sirus.Application.Entities;

public class Feature : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }

   
    private bool _done;
    public bool Done
    {
        get => _done;
        set
        {
            if (value && _done == false)
            {
                DomainEvents.Add(new FeatureCompletedEvent(this));
            }

            _done = value;
        }
    }

    public List<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();
}

public class FeatureCompletedEvent : DomainEvent
{
    public FeatureCompletedEvent(Feature item)
    {
        Item = item;
    }

    public Feature Item { get; }
}
