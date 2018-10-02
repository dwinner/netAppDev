using System;

namespace ListSamples
{
   [Serializable]
   public class Racer : IFormattable, IComparable<Racer>
   {
      public int Id { get; private set; }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string Country { get; set; }

      public int Wins { get; set; }

      public Racer(int id, string firstName, string lastName, string country, int wins)
      {
         Id = id;
         FirstName = firstName;
         LastName = lastName;
         Country = country;
         Wins = wins;
      }

      public Racer(int id, string firstName, string lastName, string country)
         : this(id, firstName, lastName, country, 0) { }

      public int CompareTo(Racer other)
      {
         if (other == null)
            return -1;
         int compare = string.Compare(LastName, other.LastName, StringComparison.CurrentCulture);
         return compare == 0 ? string.Compare(FirstName, other.FirstName, StringComparison.CurrentCulture) : compare;
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
            case null:
            case "N":   // имя и фамилия
               return ToString();
            case "F":   // имя
               return FirstName;
            case "L":   // фамилия
               return LastName;
            case "W":   // количество побед
               return string.Format("{0}, Wins: {1}", ToString(), Wins);
            case "C":   // страна
               return string.Format("{0}, Country: {1}", ToString(), Country);
            case "A":   // все вместе
               return string.Format("{0}, {1} Wins: {2}", ToString(), Country, Wins);
            default:
               throw new FormatException(string.Format(formatProvider, "Format {0} is not supported", format));
         }
      }

      public string ToString(string format)
      {
         return ToString(format, null);
      }
   }
}