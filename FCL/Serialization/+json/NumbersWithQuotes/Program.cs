#nullable disable
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NumbersWithQuotes;

internal static class Program
{
   private static void Main()
   {
      var options = new JsonSerializerOptions
      {
         WriteIndented = true,
         NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
      };

      var json = JsonSerializer.Serialize(new Point { X = 2, Y = 3 }, options);
      var p2 = (Point)JsonSerializer.Deserialize(json, typeof(Point), options);

      Console.WriteLine(json);
      Console.WriteLine(p2);
   }
}

public class Point
{
   public int X { get; set; }
   public int Y { get; set; }
   public override string ToString() => $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
}