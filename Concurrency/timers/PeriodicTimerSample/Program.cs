var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
StartPeriodicOperation();

Console.ReadLine();
return;

async void StartPeriodicOperation()
{
   while (await timer.WaitForNextTickAsync().ConfigureAwait(false))
   {
      Console.WriteLine("Tick"); // Do some action
   }
}