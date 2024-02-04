var task = Run(() =>
{
   Thread.Sleep(5000);
   return 42;
});

Console.WriteLine(task.Result);

return;

Task<TResult> Run<TResult>(Func<TResult> function)
{
   var tcs = new TaskCompletionSource<TResult>();
   var thread = new Thread(() =>
   {
      try
      {
         tcs.SetResult(function());
      }
      catch (Exception ex)
      {
         tcs.SetException(ex);
      }
   });
   thread.Start();

   return tcs.Task;
}