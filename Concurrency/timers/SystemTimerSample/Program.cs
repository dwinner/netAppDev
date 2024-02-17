using Timer = System.Timers.Timer;

var tmr = new Timer(); // Doesn't require any args
tmr.Interval = 500;
tmr.Elapsed += OnElapsed; // Uses an event instead of a delegate
tmr.Start(); // Start the timer
Console.ReadLine();
tmr.Stop(); // Stop the timer
Console.ReadLine();
tmr.Start(); // Restart the timer
Console.ReadLine();
tmr.Dispose(); // Permanently stop the timer
return;

static void OnElapsed(object? sender, EventArgs e)
{
   Console.WriteLine("Tick");
}