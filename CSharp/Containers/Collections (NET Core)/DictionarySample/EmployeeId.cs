using System;

namespace DictionarySample
{
   public readonly struct EmployeeId : IEquatable<EmployeeId>
   {
      private readonly char _prefix;
      private readonly int _number;

      public EmployeeId(string id)
      {
         if (string.IsNullOrEmpty(id))
         {
            throw new ArgumentException($"Argument '{nameof(id)}' cannot be empty", nameof(id));
         }

         _prefix = id.ToUpper()[0];
         const int strThreshold = 7;
         var last = id.Length > strThreshold ? strThreshold : id.Length;
         try
         {
            _number = int.Parse(id[1..last]);
         }
         catch (FormatException fmtEx)
         {
            throw new EmployeeIdException("Invalid EmployeeId format", fmtEx);
         }
      }

      public override string ToString() => $"{_prefix}{_number,6:000000}";

      public override int GetHashCode() => (_number ^ (_number << 16)) * 0x1505_1505;

      public bool Equals(EmployeeId other) => _prefix == other._prefix && _number == other._number;

      public override bool Equals(object? obj) => obj != null && Equals((EmployeeId)obj);

      public static bool operator ==(EmployeeId left, EmployeeId right) => left.Equals(right);

      public static bool operator !=(EmployeeId left, EmployeeId right) => !(left == right);
   }
}