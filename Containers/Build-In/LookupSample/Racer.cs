using System;

namespace LookupSample
{
   [Serializable]
   public class Racer : IComparable<Racer>, IFormattable
   {
      public int Id { get; private set; }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string Country { get; set; }

      public int Wins { get; set; }

      public Racer(int id, string firstName, string lastName, string country = null, int wins = 0)
      {
         Id = id;
         FirstName = firstName;
         LastName = lastName;
         Country = country;
         Wins = wins;
      }

      public override string ToString()
      {
         return string.Format("{0} {1}", FirstName, LastName);
      }

      public string ToString(string format, IFormatProvider formatProvider)
      {
         if (format == null)
            format = "N";
         switch (format.ToUpper())
         {
            case "N":
               return ToString();
            case "F":
               return FirstName;
            case "L":
               return LastName;
            case "W":
               return string.Format("{0}, Wins: {1}", ToString(), Wins);
            case "C":
               return string.Format("{0}, Country: {1}", ToString(), Country);
            case "A":
               return string.Format("{0}, {1} Wins: {2}", ToString(), Country, Wins);
            default:
               throw new FormatException(string.Format(formatProvider, "Format {0} is not supported", format));
         }
      }

      public string ToString(string format)
      {
         return ToString(format, null);
      }

      public int CompareTo(Racer other)
      {
         int compare = string.Compare(LastName, other.LastName, StringComparison.Ordinal);
         return compare == 0 ? string.Compare(FirstName, other.FirstName, StringComparison.Ordinal) : compare;
      }
   }
}