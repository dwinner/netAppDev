using System.Xml.Serialization;

namespace PropertySurrogate;

public class CountryStats
{
   [XmlIgnore]
   public Dictionary<string, string> Capitals { get; private set; } = new();

   public (string, string)[] CapitalsSerializable
   {
      get
      {
         return Capitals.Keys.Select(country =>
            (country, Capitals[country])).ToArray();
      }
      set
      {
         Capitals = value.ToDictionary(x => x.Item1, x => x.Item2);
      }
   }
}