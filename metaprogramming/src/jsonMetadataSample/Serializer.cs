using System.Text;

namespace JsonMetadata;

public static class Serializer
{
   public static string SerializeToJson(object instance)
   {
      var stringBuilder = new StringBuilder();
      var type = instance.GetType();
      var properties = type.GetProperties();
      stringBuilder.Append("{\n");
      var first = true;

      foreach (var property in properties)
      {
         if (!first)
         {
            stringBuilder.Append(",\n");
         }

         first = false;
         var propertyName = property.Name;
         var propertyValue = property.GetValue(instance);
         stringBuilder.Append($"   \"{propertyName}\": \"{propertyValue}\"");
      }

      stringBuilder.Append("\n}");

      return stringBuilder.ToString();
   }
}