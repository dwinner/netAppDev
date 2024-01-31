namespace FuncDisposable;

public class Disposable : IDisposable
{
   private Action? _onDispose;
   private Disposable(Action? onDispose) => _onDispose = onDispose;

   public void Dispose()
   {
      _onDispose?.Invoke();
      _onDispose = null;
   }

   public static Disposable Create(Action onDispose) => new(onDispose);
}