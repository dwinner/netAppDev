var factory = new TaskFactory(
   TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent,
   TaskContinuationOptions.None);

var task1 = factory.StartNew(() => Console.WriteLine("foo"));
var task2 = factory.StartNew(() => Console.WriteLine("far"));

Console.ReadLine();