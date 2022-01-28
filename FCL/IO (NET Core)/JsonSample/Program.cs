using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

Category appetizers = new("Appetizers");
appetizers.Items.Add(new Item("Dungeness Crab Cocktail", "Classic cocktail sauce", 27M));
appetizers.Items.Add(new Item("Almond Crusted Scallops", "Almonds, Parmesan, chive beurre blanc", 19M));

Category dinner = new("Dinner");
dinner.Items.Add(new Item("Grilled King Salmon", "Lemon chive buerre blanc", 49M));

Card card = new("The Restaurant");
card.Categories.Add(appetizers);
card.Categories.Add(dinner);

string json = SerializeJson(card);
DeserializeJson(json);
UseDom(json);
UseReader(json);
UseWriter();

string SerializeJson(Card aCard)
{
   Console.WriteLine(nameof(SerializeJson));
   JsonSerializerOptions options = new()
   {
      WriteIndented = true,
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
      AllowTrailingCommas = true
      // ReferenceHandler = ReferenceHandler.Preserve
   };
   string lJson = JsonSerializer.Serialize(aCard, options);
   Console.WriteLine(lJson);
   Console.WriteLine();
   return lJson;
}

void DeserializeJson(string aJson)
{
   Console.WriteLine(nameof(DeserializeJson));
   JsonSerializerOptions options = new()
   {
      PropertyNameCaseInsensitive = true
   };
   var lCard = JsonSerializer.Deserialize<Card>(aJson, options);
   if (lCard is null)
   {
      Console.WriteLine("no card deserialized");
      return;
   }

   Console.WriteLine($"{lCard.Title}");
   foreach (var category in lCard.Categories)
   {
      Console.WriteLine($"\t{category.Title}");
      foreach (var item in category.Items)
      {
         Console.WriteLine($"\t\t{item.Title}");
      }
   }

   Console.WriteLine();
}

void UseDom(string aJson)
{
   Console.WriteLine(nameof(UseDom));

   using JsonDocument document = JsonDocument.Parse(aJson);
   var titleElement = document.RootElement.GetProperty("title");
   Console.WriteLine(titleElement);
   foreach (var item
      in document.RootElement.GetProperty("categories").EnumerateArray()
         .SelectMany(category => category.GetProperty("items").EnumerateArray()))
   {
      foreach (var property in item.EnumerateObject())
      {
         Console.WriteLine($"{property.Name} {property.Value}");
      }

      Console.WriteLine($"{item.GetProperty("title")}");
   }
}

void UseReader(string aJson)
{
   Console.WriteLine(nameof(UseReader));

   var isNextPrice = false;
   var isNextTitle = false;
   string? title = default;
   byte[] data = Encoding.UTF8.GetBytes(aJson);
   Utf8JsonReader reader = new(data);
   while (reader.Read())
   {
      switch (reader.TokenType)
      {
         case JsonTokenType.PropertyName when reader.GetString() == "title":
            isNextTitle = true;
            break;

         case JsonTokenType.String when isNextTitle:
            title = reader.GetString();
            isNextTitle = false;
            break;

         case JsonTokenType.PropertyName when reader.GetString() == "price":
            isNextPrice = true;
            break;

         case JsonTokenType.Number when isNextPrice && reader.TryGetDecimal(out var price):
            Console.WriteLine($"{title}, price: {price:C}");
            isNextPrice = false;
            break;
      }
   }

   Console.WriteLine();
}

void UseWriter()
{
   Console.WriteLine(nameof(UseWriter));
   using MemoryStream stream = new();

   JsonWriterOptions options = new()
   {
      Indented = true
   };
   using (Utf8JsonWriter writer = new(stream, options))
   {
      writer.WriteStartArray();
      writer.WriteStartObject();
      writer.WriteStartObject("Book");
      writer.WriteString("Title", "Professional C# and .NET");
      writer.WriteString("Subtitle", "2021 Edition");
      writer.WriteEndObject();
      writer.WriteEndObject();
      writer.WriteStartObject();
      writer.WriteStartObject("Book");
      writer.WriteString("Title", "Professional C# 7 and .NET Core 2");
      writer.WriteString("Subtitle", "2018 Edition");
      writer.WriteEndObject();
      writer.WriteEndObject();
      writer.WriteEndArray();
   }

   string lJson = Encoding.UTF8.GetString(stream.ToArray());
   Console.WriteLine(lJson);
   Console.WriteLine();
}

public record Item(string Title, string Text, decimal Price);

public record Category(string Title)
{
   public IList<Item> Items { get; init; } = new List<Item>();
}

public record Card(string Title)
{
   public IList<Category> Categories { get; init; } = new List<Category>();
}