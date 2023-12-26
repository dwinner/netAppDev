using System;

namespace LookupSample
{
   public record Racer(int Id, string FirstName, string LastName, string Country, int Wins = 0)
      : IComparable<Racer>, IFormattable
   {
      public int CompareTo(Racer? other)
      {
         var compare = LastName?.CompareTo(other?.LastName) ?? -1;
         if (compare == 0)
         {
            return FirstName?.CompareTo(other?.FirstName) ?? -1;
         }

         return compare;
      }

      public string ToString(string? format, IFormatProvider? formatProvider) =>
         format?.ToUpper() switch
         {
            null => ToString(),
            "N" => ToString(),
            "F" => FirstName,
            "L" => LastName,
            "W" => $"{ToString()}, Wins: {Wins}",
            "C" => Country,
            "A" => $"{ToString()}, Country: {Country}, Wins: {Wins}",
            _ => throw new FormatException(string.Format(formatProvider,
               "Format {0} is not supported", format))
         };

      public override string ToString() => $"{FirstName} {LastName}";

      public string? ToString(string format) => ToString(format, null);
   }
}