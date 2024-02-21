var spinLock = new SpinLock(true); // Enable owner tracking
var lockTaken = false;
try
{
   spinLock.Enter(ref lockTaken);
   Console.WriteLine("lock taken");
}
finally
{
   if (lockTaken)
   {
      spinLock.Exit();
   }
}