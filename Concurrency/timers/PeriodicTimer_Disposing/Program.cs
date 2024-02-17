var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
StartPeriodicOperation();

async void StartPeriodicOperation()
{
   while (await timer.WaitForNextTickAsync().ConfigureAwait(false))
   {
      Console.WriteLine("Tick"); // Do some action
   }
}

Console.WriteLine("Press enter to stop");
Console.ReadLine();
timer.Dispose();