using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fundamentals;

/// <summary>
///    Represents global configuration for JSON serialization.
/// </summary>
public static class Globals
{
   private static JsonSerializerOptions? _jsonSerializerOptions;

   /// <summary>
   ///    Gets the global <see cref="JsonSerializerOptions" /> - it can be null if not initialized.
   /// </summary>
   public static JsonSerializerOptions JsonSerializerOptions
   {
      get
      {
         if (_jsonSerializerOptions is null)
         {
            Configure();
         }

         return _jsonSerializerOptions!;
      }
   }

   /// <summary>
   ///    Configure the globals.
   /// </summary>
   public static void Configure()
   {
      if (_jsonSerializerOptions is not null)
      {
         return;
      }

      _jsonSerializerOptions = new JsonSerializerOptions
      {
         PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
         DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
         Converters = { new ConceptAsJsonConverterFactory() }
      };
   }
}