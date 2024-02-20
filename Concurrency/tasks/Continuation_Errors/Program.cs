var task1 = Task.Factory.StartNew(() => throw null!);
var error = task1.ContinueWith(ant => Console.Write(ant.Exception),
   TaskContinuationOptions.OnlyOnFaulted);
var ok = task1.ContinueWith(ant => Console.Write("Success!"),
   TaskContinuationOptions.NotOnFaulted);
error.Wait();