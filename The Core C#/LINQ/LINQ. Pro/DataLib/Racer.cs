using System;
using System.Collections.Generic;
using System.Globalization;

namespace DataLib
{
   [Serializable]
   public class Racer : IComparable<Racer>, IFormattable
   {
      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string Country { get; set; }

      public int Wins { get; set; }

      public int Starts { get; set; }

      public IEnumerable<int> Years { get; private set; }

      public IEnumerable<string> Cars { get; private set; }

      public Racer(string firstName, string lastName, string country, int wins, int starts, IEnumerable<int> years, IEnumerable<string> cars)
      {
         FirstName = firstName;
         LastName = lastName;
         Country = country;
         Wins = wins;
         Starts = starts;
         Years = new List<int>(years);
         Cars = new List<string>(cars);
      }

      public Racer(string firstName, string lastName, string country, int starts, int wins)
         : this(firstName, lastName, country, starts, wins, null, null) { }

      public int CompareTo(Racer other)
      {
         return other == null ? -1 : string.Compare(LastName, other.LastName, StringComparison.CurrentCulture);
      }

      public string ToString(string format, IFormatProvider formatProvider)
      {
         switch (format)
         {
            case null:
            case "N":
               return ToString();
            case "F":
               return FirstName;
            case "L":
               return LastName;
            case "C":
               return Country;
            case "S":
               return Starts.ToString(CultureInfo.CurrentCulture);
            case "W":
               return Wins.ToString(CultureInfo.CurrentCulture);
            case "A":
               return string.Format("{0}, {1}, {2}; starts: {3}, wins: {4}",
                  FirstName, LastName, Country, Starts, Wins);
            default:
               throw new FormatException(string.Format("Format {0} not supported", format));
         }
      }

      public string ToString(string format)
      {
         return ToString(format, null);
      }

      public override string ToString()
      {
         return string.Format("{0} {1}", FirstName, LastName);
      }
   }
}