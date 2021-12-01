using System.Collections.Generic;
using static ChainViaEnums.EnumUtils;

namespace ChainViaEnums
{
   public sealed class Mail
   {
      private static long _counter;
      private readonly long _id = _counter++;

      private Mail()
      {
      }

      public Address Address { get; init; }

      public GeneralDelivery Delivery { get; init; }

      public Readability Readability { get; init; }

      public ReturnAddress ReturnAddress { get; init; }

      public Scannability Scannability { get; init; }

      public static Mail NewRandomMail =>
         new()
         {
            Delivery = GetRandomEnumValue<GeneralDelivery>(),
            Scannability = GetRandomEnumValue<Scannability>(),
            Readability = GetRandomEnumValue<Readability>(),
            Address = GetRandomEnumValue<Address>(),
            ReturnAddress = GetRandomEnumValue<ReturnAddress>()
         };

      public static IEnumerable<Mail> Generate(int count)
      {
         while (count-- > 0)
         {
            yield return NewRandomMail;
         }
      }

      public override string ToString() => $"Mail {_id}";

      public string GetDetails() =>
         $"{ToString()}, General Delivery: {Delivery}, Address Scanability: {Scannability}, Address Readability: {Readability}, Address Address: {Address}, Return address: {ReturnAddress}";
   }
}