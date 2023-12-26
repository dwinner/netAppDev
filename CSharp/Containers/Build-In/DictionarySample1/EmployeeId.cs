using System;

namespace DictionarySample
{
   [Serializable]
   public struct EmployeeId : IEquatable<EmployeeId>
   {
      private readonly char _prefix;
      private readonly int _number;

      public EmployeeId(string id)
      {
         // Contract.Requires<ArgumentNullException>(id != null);
         if (id == null)
            throw new ArgumentNullException("id");
         _prefix = id.ToUpper()[0];
         int numLength = id.Length - 1;
         try
         {
            _number = int.Parse(id.Substring(1, numLength > 6 ? 6 : numLength));
         }
         catch (FormatException formatEx)
         {
            throw new EmployeeIdException("Invalid EmployeeId format", formatEx);
         }
      }

      public override string ToString()
      {
         return _prefix + string.Format("{0,6:000000}", _number);
      }

      public bool Equals(EmployeeId other)
      {         
         return _prefix == other._prefix && _number == other._number;
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj))
            return false;
         return obj is EmployeeId && Equals((EmployeeId)obj);
      }

      public override int GetHashCode()
      {
         return (_number ^ _number << 16) * 0x15051505;
      }

      public static bool operator ==(EmployeeId left, EmployeeId right)
      {
         return left.Equals(right);
      }

      public static bool operator !=(EmployeeId left, EmployeeId right)
      {
         return !left.Equals(right);
      }
   }
}