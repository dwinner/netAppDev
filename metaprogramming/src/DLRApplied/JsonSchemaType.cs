using System.Dynamic;
using NJsonSchema;

namespace DLRApplied;

public class JsonSchemaType(JsonSchema schema) : DynamicObject
{
   private readonly IDictionary<string, object?> _values = new Dictionary<string, object?>();

   public override bool TrySetMember(SetMemberBinder binder, object? value)
   {
      var propertyName = binder.Name;
      if (!schema.ActualProperties.ContainsKey(propertyName))
      {
         return false;
      }

      ValidateType(propertyName, value);
      _values[propertyName] = value;

      return true;
   }

   public override bool TryGetMember(GetMemberBinder binder, out object? result)
   {
      var propertyName = binder.Name;
      if (!schema.ActualProperties.ContainsKey(propertyName))
      {
         result = null!;
         return false;
      }

      result = _values.TryGetValue(propertyName, out var value)
         ? value
         : null!;

      return true;
   }

   public override bool TryConvert(ConvertBinder binder, out object? result)
   {
      if (binder.Type == typeof(Dictionary<string, object?>))
      {
         var returnValue = new Dictionary<string, object?>(_values);
         var missingProperties = schema.ActualProperties.Where(pair => _values.All(kvp => pair.Key != kvp.Key));
         foreach (var property in missingProperties)
         {
            object defaultValue = property.Value.Type switch
            {
               JsonObjectType.Array => Enumerable.Empty<object>(),
               JsonObjectType.Boolean => false,
               JsonObjectType.Integer => 0,
               JsonObjectType.Number => 0,
               _ => null!
            };

            returnValue[property.Key] = defaultValue;
         }

         result = returnValue;
         return true;
      }

      return base.TryConvert(binder, out result);
   }

   public override IEnumerable<string> GetDynamicMemberNames() => schema.ActualProperties.Keys;

   private void ValidateType(string property, object? value)
   {
      if (value is null)
      {
         return;
      }

      var schemaType = GetSchemaTypeFrom(value.GetType());
      if (!schema.ActualProperties[property].Type.HasFlag(schemaType))
      {
         throw new InvalidPropertyTypeException(schema.Title, property);
      }
   }

   private static JsonObjectType GetSchemaTypeFrom(Type type) =>
      type switch
      {
         not null when type == typeof(string) => JsonObjectType.String,
         not null when type == typeof(DateOnly) => JsonObjectType.String,
         not null when type == typeof(int) => JsonObjectType.Integer,
         not null when type == typeof(float) => JsonObjectType.Number,
         not null when type == typeof(double) => JsonObjectType.Number,
         _ => JsonObjectType.Object
      };
}