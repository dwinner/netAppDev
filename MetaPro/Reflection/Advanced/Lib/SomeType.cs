using System;
using System.Reflection;
using static System.Reflection.BindingFlags;

namespace Lib
{
   public sealed class SomeType
   {
      private int _someField;
      public const BindingFlags DefaultCallingFlags = DeclaredOnly | Public | NonPublic | Instance;

      public SomeType(ref int x)
      {
         x *= 2;
      }

      public SomeType()
      {         
      }

      public int SomeProp
      {
         get { return _someField; }
         set
         {
            if (value < 1) throw new ArgumentOutOfRangeException(nameof(value));
            _someField = value;
         }
      }

      public override string ToString()
      {
         return _someField.ToString();
      }

      public event EventHandler SomeEvent;

      private void NoCompilerWarnings()
      {
         SomeEvent?.ToString();
      }
   }
}