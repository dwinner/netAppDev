var task = Task.Run(() =>
{
   Console.WriteLine("Task started");
   Thread.Sleep(2000);
   Console.WriteLine("Foo");
});
Console.WriteLine(task.IsCompleted); // False
task.Wait(); // Blocks until task is complete