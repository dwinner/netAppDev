// First interval = 5000ms; subsequent intervals = 1000ms

using var _ = new Timer(Tick, "tick...", 5000, 1000);
Console.WriteLine("Press Enter to stop");
Console.ReadLine();

return;

static void Tick(object? data)
{
   // This runs on a pooled thread
   Console.WriteLine(data); // Writes "tick..."
}