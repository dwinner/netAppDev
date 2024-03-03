#nullable disable
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CustomizingConversion_Default;

internal static class Program
{
   private static void Main()
   {
      var json = """
                 
                 	{
                 		"Id":27182,
                 		"Name":"Sara",
                 		"Born":464572800
                 	}
                 """;

      var sara = JsonSerializer.Deserialize<Person>(json);
      Console.WriteLine(sara);
   }
}

public class Person
{
   public int Id { get; set; }

   public string Name { get; set; }

   [JsonConverter(typeof(UnixTimestampConverter))]
   public DateTime Born { get; set; }

   public override string ToString() => $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Born)}: {Born}";
}

public class UnixTimestampConverter : JsonConverter<DateTime>
{
   private static readonly DateTime Epoch = new(1970, 1, 1);

   public override DateTime Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
   {
      if (reader.TryGetInt32(out var timestamp))
      {
         return Epoch.AddSeconds(timestamp);
      }

      throw new InvalidOperationException("Expected the timestamp as a number.");
   }

   public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
   {
      var timestamp = (int)(value - Epoch).TotalSeconds;
      writer.WriteNumberValue(timestamp);
   }
}