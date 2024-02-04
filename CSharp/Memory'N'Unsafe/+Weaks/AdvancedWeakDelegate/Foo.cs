namespace AdvancedWeakDelegate;

public class Foo
{
   private readonly WeakDelegate<EventHandler> _click = new();

   public event EventHandler Click
   {
      add => _click.Combine(value);
      remove => _click.Remove(value);
   }

   protected virtual void OnClick(EventArgs e) => _click.Target?.Invoke(this, e);
}