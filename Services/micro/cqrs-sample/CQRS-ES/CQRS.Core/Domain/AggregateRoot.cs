using CQRS.Core.Events;

namespace CQRS.Core.Domain;

public abstract class AggregateRoot
{
   private readonly List<BaseEvent> _changes = [];
   protected Guid Id;

   public Guid AggregateId => Id;

   public int Version { get; set; } = -1;

   public IEnumerable<BaseEvent> GetUncommittedChanges() => _changes;

   public void MarkChangesAsCommitted()
   {
      _changes.Clear();
   }

   private void ApplyChange(BaseEvent anEvent, bool isNew)
   {
      var method = GetType().GetMethod("Apply", [anEvent.GetType()]);
      if (method == null)
      {
         throw new ArgumentNullException(nameof(method),
            $"The Apply method was not found in the aggregate for {anEvent.GetType().Name}!");
      }

      method.Invoke(this, [anEvent]);
      if (isNew)
      {
         _changes.Add(anEvent);
      }
   }

   protected void RaiseEvent(BaseEvent anEvent)
   {
      ApplyChange(anEvent, true);
   }

   public void ReplayEvents(IEnumerable<BaseEvent> events)
   {
      foreach (var evt in events)
      {
         ApplyChange(evt, false);
      }
   }
}