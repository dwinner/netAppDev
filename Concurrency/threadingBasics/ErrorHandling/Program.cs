using System.Diagnostics;

var thread = new Thread(Go);
thread.Start();
thread.Join();

return;

void Go()
{
   try
   {
      throw null;    // The NullReferenceException will get caught below
   }
   catch (Exception ex)
   {
      Debug.WriteLine(ex.Message);
   }
}