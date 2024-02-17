var starter = new ManualResetEvent(false);

var reg = ThreadPool.RegisterWaitForSingleObject(starter, Go, "Some Data", -1, true);
Thread.Sleep(5000);
Console.WriteLine("Signaling worker...");
starter.Set();
Console.ReadLine();
reg.Unregister(starter); // Clean up when we’re done.
return;

void Go(object? data, bool timedOut)
{
   Console.WriteLine("Started - {0}", data);
   // Perform task...
}