namespace Changing_state_over_time;

public abstract class Event<T>
{
   public string EventName { get; set; }
   public string EventDescription { get; set; }
   public abstract T ApplyEventTo(T thing);
}