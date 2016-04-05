using System;

namespace PointerPlayground2
{
   internal static class Program
   {
      private static unsafe void Main()
      {
         Console.WriteLine("Size of Currency struct is {0}", sizeof(CurrencyStruct));
         CurrencyStruct amount1, amount2;
         CurrencyStruct* pAmount = &amount1;
         long* pDollars = &(pAmount->Dollars);
         byte* pCents = &(pAmount->Cents);

         Console.WriteLine("Address of amount1 is 0x{0:X}", (uint)&amount1);
         Console.WriteLine("Address of amount2 is 0x{0:X}", (uint)&amount2);
         Console.WriteLine("Address of pAmt is 0x{0:X}", (uint)&pAmount);
         Console.WriteLine("Address of pDollars is 0x{0:X}", (uint)&pDollars);
         Console.WriteLine("Address of pCents is 0x{0:X}", (uint)&pCents);
         pAmount->Dollars = 20;
         *pCents = 50;
         Console.WriteLine("amount1 contains {0}", amount1);
         --pAmount;   // Note: это должно переустановить указатель на amount2
         Console.WriteLine("amount2 has address 0x{0:X} and contains {1}", (uint)pAmount, *pAmount);
         // Note: Выполнить некоторое интеллектуальное приведение, чтобы установить
         // указатель pCents на значение центов внутри amount2
         var pTempCurrency = (CurrencyStruct*)pCents;
         pCents = (byte*)(--pTempCurrency);
         Console.WriteLine("Address of pCents is now 0x{0:X}", (uint)&pCents);
         Console.WriteLine("\nNow with classes");
         // Пробуем указатели с классами
         var amount3 = new CurrencyClass();

         fixed (long* pDollars2 = &(amount3.Dollars))
         fixed (byte* pCents2 = &(amount3.Cents))
         {
            Console.WriteLine("amount3.Dollars has address 0x{0:X}", (uint)pDollars2);
            Console.WriteLine("amount3.Cents has address 0x{0:X}", (uint)pCents2);
            *pDollars2 = -100;
            Console.WriteLine("amount3 contains {0}", amount3);
         }

         Console.ReadLine();
      }
   }

   struct CurrencyStruct
   {
      public long Dollars;
      public byte Cents;

      public override string ToString()
      {
         return string.Format("${0}.{1}", Dollars, Cents);
      }
   }

   class CurrencyClass
   {
      public long Dollars;
      public byte Cents;

      public override string ToString()
      {
         return string.Format("${0}.{1}", Dollars, Cents);
      }
   }
}
