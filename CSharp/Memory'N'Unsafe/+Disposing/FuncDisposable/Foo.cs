namespace FuncDisposable;

internal class Foo
{
   private int _suspendCount;

   public IDisposable SuspendEvents()
   {
      _suspendCount++;
      return Disposable.Create(() => _suspendCount--);
   }

   public void FireSomeEvent()
   {
      Console.WriteLine(_suspendCount == 0 
         ? "Event would fire" 
         : "Event suppressed");
   }
}