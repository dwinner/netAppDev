namespace CountdownByHand;

public class Countdown
{
   private readonly object _locker = new();
   private int _value;

   public Countdown()
   {
   }

   public Countdown(int initialCount) => _value = initialCount;

   public void Signal()
   {
      AddCount(-1);
   }

   public void AddCount(int amount)
   {
      lock (_locker)
      {
         _value += amount;
         if (_value <= 0)
         {
            Monitor.PulseAll(_locker);
         }
      }
   }

   public void Wait()
   {
      lock (_locker)
      {
         while (_value > 0)
         {
            Monitor.Wait(_locker);
         }
      }
   }
}