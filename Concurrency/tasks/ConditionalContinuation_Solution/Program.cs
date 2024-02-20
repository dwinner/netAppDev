var t1 = Task.Factory.StartNew(() => Console.WriteLine("nothing awry here"));

var fault = t1.ContinueWith(ant => Console.WriteLine("fault"),
   TaskContinuationOptions.OnlyOnFaulted);

var t3 = fault.ContinueWith(ant => Console.WriteLine("t3"),
   TaskContinuationOptions.NotOnCanceled); // Does not execute

Console.ReadLine();