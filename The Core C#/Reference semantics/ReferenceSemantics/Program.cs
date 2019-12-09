using System;
using System.Linq;

namespace ReferenceSemantics
{
   internal static class Program
   {
      private static void Main()
      {
         UseMember();

         UseRefMember();

         UseReadonlyRefMember();

         UseItemOfContainer();

         UseArrayOfContainer();
      }

      private static void UseMember()
      {
         Console.WriteLine(nameof(UseMember));
         var data = new Data(11);
         //var n = data.GetNumber();
         //n = 42;
         data.Show();
         Console.WriteLine();
      }

      private static void UseRefMember()
      {
         Console.WriteLine(nameof(UseRefMember));
         var d = new Data(11);
         ref var n = ref d.GetNumber();
         n = 42;
         d.Show();

         //ref readonly int n2 = d.GetNumber();
         //n2 = 42;
         Console.WriteLine();
      }

      private static void UseReadonlyRefMember()
      {
         Console.WriteLine(nameof(UseReadonlyRefMember));
         var d = new Data(11);
         var n = d.GetReadonlyNumber(); // create a copy
         Console.WriteLine(n);
         // n = 42;
         d.Show();

         ref readonly var n2 = ref d.GetReadonlyNumber();
         // n2 = 42; // not allowed
         Console.WriteLine(n2);
      }

      private static void UseItemOfContainer()
      {
         Console.WriteLine(nameof(UseItemOfContainer));
         var c = new Container(Enumerable.Range(0, 10).Select(x => x).ToArray());
         ref var item = ref c.GetItem(3);
         item = 33;
         Console.WriteLine(c);
      }

      private static void UseArrayOfContainer()
      {
         Console.WriteLine(nameof(UseArrayOfContainer));
         var c = new Container(Enumerable.Range(0, 10).Select(x => x).ToArray());
         ref var d1 = ref c.GetData();
         d1 = new[] {4, 5, 6};
         Console.WriteLine(c);
      }
   }
}