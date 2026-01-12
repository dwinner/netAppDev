var x = 0;
var lockObj = new Lock();

for (var i = 0; i < 5; i++)
{
   var thread = new Thread(Print)
   {
      Name = $"Thread {i}"
   };
   thread.Start();
}

Console.WriteLine("Hello, World!");

return;

void Print()
{
   using (lockObj.EnterScope())
   {
      x = 1;
      for (var i = 0; i < 5; i++)
      {
         Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
         x++;
         Thread.Sleep(TimeSpan.FromMilliseconds(100));
      }
   }
}