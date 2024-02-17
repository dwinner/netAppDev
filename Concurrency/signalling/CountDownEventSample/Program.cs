var countdown = new CountdownEvent(3); // Initialize with "count" of 3.

new Thread(SaySomething).Start("I am thread 1");
new Thread(SaySomething).Start("I am thread 2");
new Thread(SaySomething).Start("I am thread 3");

countdown.Wait(); // Blocks until Signal has been called 3 times
Console.WriteLine("All threads have finished speaking!");
return;

void SaySomething(object? thing)
{
   Thread.Sleep(1000);
   Console.WriteLine(thing);
   countdown.Signal();
}