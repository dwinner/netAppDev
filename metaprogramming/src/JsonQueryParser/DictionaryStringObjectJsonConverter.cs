using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonQueryParser;

public class DictionaryStringObjectJsonConverter : JsonConverter<Dictionary<string, object>>
{
   public override Dictionary<string, object> Read(
      ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
   {
      if (reader.TokenType != JsonTokenType.StartObject)
      {
         throw new JsonException(
            $"JsonTokenType was of type {reader.TokenType}, only objects are supported");
      }

      var dictionary = new Dictionary<string, object>();
      while (reader.Read())
      {
         if (reader.TokenType == JsonTokenType.EndObject)
         {
            return dictionary;
         }

         if (reader.TokenType != JsonTokenType.PropertyName)
         {
            throw new JsonException("JsonTokenType was not PropertyName");
         }

         var propertyName = reader.GetString();
         if (string.IsNullOrWhiteSpace(propertyName))
         {
            throw new JsonException("Failed to get property name");
         }

         reader.Read();
         var extractedValue = ExtractValue(ref reader, options);
         if (extractedValue != null)
         {
            dictionary.Add(propertyName, extractedValue);
         }
      }

      return dictionary;
   }

   public override void Write(Utf8JsonWriter writer, Dictionary<string, object> value, JsonSerializerOptions options)
   {
      JsonSerializer.Serialize(writer, value, options);
   }

   private object? ExtractValue(ref Utf8JsonReader reader, JsonSerializerOptions options)
   {
      switch (reader.TokenType)
      {
         case JsonTokenType.String:
            return reader.TryGetDateTime(out var date)
               ? date
               : reader.GetString();

         case JsonTokenType.False:
            return false;

         case JsonTokenType.True:
            return true;

         case JsonTokenType.Null:
            return null;

         case JsonTokenType.Number:
            return reader.TryGetInt32(out var result)
               ? result
               : reader.GetDecimal();

         case JsonTokenType.StartObject:
            return Read(ref reader, null, options);

         case JsonTokenType.StartArray:
            var list = new List<object>();
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
               var extractedValue = ExtractValue(ref reader, options);
               if (extractedValue != null)
               {
                  list.Add(extractedValue);
               }
            }

            return list;

         default:
            throw new JsonException($"'{reader.TokenType}' is not supported");
      }
   }
}