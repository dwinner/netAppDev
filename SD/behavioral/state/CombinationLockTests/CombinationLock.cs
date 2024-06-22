namespace CombinationLockTests;

public class CombinationLock
{
   private readonly int[] _combination;

   private int _digitsEntered;
   private bool _failed;

   public string Status { get; private set; } = string.Empty;

   public CombinationLock(int[] combination)
   {
      _combination = combination;
      Reset();
   }

   private void Reset()
   {
      Status = "LOCKED";
      _digitsEntered = 0;
      _failed = false;
   }

   public void EnterDigit(int digit)
   {
      if (Status == "LOCKED")
      {
         Status = string.Empty;
      }

      Status += digit.ToString();
      if (_combination[_digitsEntered] != digit)
      {
         _failed = true;
      }

      _digitsEntered++;

      if (_digitsEntered == _combination.Length)
      {
         Status = _failed ? "ERROR" : "OPEN";
      }
   }
}