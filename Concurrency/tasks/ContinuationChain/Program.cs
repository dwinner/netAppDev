Task.Factory.StartNew(() => 8)
   .ContinueWith(ant => ant.Result * 2)
   .ContinueWith(ant => Math.Sqrt(ant.Result))
   .ContinueWith(ant => Console.WriteLine(ant.Result)); // 4