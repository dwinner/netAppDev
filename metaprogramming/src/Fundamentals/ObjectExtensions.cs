using System.Collections;
using System.Dynamic;
using System.Reflection;
using System.Text.Json;

namespace Fundamentals;

/// <summary>
///    Extension methods for any object.
/// </summary>
public static class ObjectExtensions
{
   /// <summary>
   ///    Deep clone an object.
   /// </summary>
   /// <param name="source">Object clone.</param>
   /// <typeparam name="T">Type of object to clone.</typeparam>
   /// <returns>Cloned instance.</returns>
   public static T Clone<T>(this T source)
   {
      if (source is null)
      {
         return default!;
      }

      if (source is ExpandoObject expandoObject)
      {
         return (T)(object)expandoObject.Clone();
      }

      if (source is Guid)
      {
         return source;
      }

      var valueType = source.GetType();
      if (valueType.IsPrimitive || valueType == typeof(string))
      {
         return source;
      }

      if (valueType.IsConcept())
      {
         return (T)ConceptFactory.CreateConceptInstance(valueType, source.GetConceptValue());
      }

      var sourceAsString = JsonSerializer.Serialize(source, Globals.JsonSerializerOptions);
      return (T)JsonSerializer.Deserialize(sourceAsString, valueType, Globals.JsonSerializerOptions)!;
   }

   /// <summary>
   ///    Ensure a <see cref="PropertyPath" /> on an object.
   /// </summary>
   /// <param name="source">Object to ensure on.</param>
   /// <param name="propertyPath"><see cref="PropertyPath" /> to ensure.</param>
   /// <param name="arrayIndexers">All <see cref="ArrayIndexer">array indexers</see>.</param>
   /// <returns>Instance of the last segment in the path.</returns>
   /// <exception cref="UnableToResolvePropertyPathOnTypeException">
   ///    Thrown if not able to resolve parts of the property path
   ///    on the type.
   /// </exception>
   public static object EnsurePath(this object source, PropertyPath propertyPath, IArrayIndexers arrayIndexers)
   {
      var currentType = source.GetType();
      var currentInstance = source;
      var currentPath = new PropertyPath(string.Empty);

      var segments = propertyPath.Segments.ToArray();
      for (var segmentIndex = 0; segmentIndex < segments.Length - 1; segmentIndex++)
      {
         var segment = segments[segmentIndex];
         currentPath += segment;
         var parentInstance = currentInstance;
         var currentPropertyInfo = currentType.GetProperties().SingleOrDefault(
               propertyInfo => propertyInfo.Name == segment.Value || propertyInfo.Name == segment.Value.ToPascalCase());
         if (currentPropertyInfo is null)
         {
            throw new UnableToResolvePropertyPathOnTypeException(currentType, propertyPath);
         }

         currentInstance = currentPropertyInfo.GetValue(currentInstance);
         if (currentInstance is null)
         {
            var newInstance = CreateInstanceOf(currentPropertyInfo.PropertyType);
            currentPropertyInfo.SetValue(parentInstance, newInstance);
            currentInstance = newInstance;
         }

         currentType = currentPropertyInfo.PropertyType;
         switch (segment)
         {
            case ArrayProperty:
            {
               var indexer = arrayIndexers.GetFor(currentPath);
               var element = (currentInstance as IEnumerable)!
                  .Cast<object>()
                  .SingleOrDefault(o =>
                  {
                     var property = o.GetType().GetProperties().SingleOrDefault(propertyInfo =>
                        propertyInfo.Name == segment.Value || propertyInfo.Name == segment.Value.ToPascalCase());
                     return property is not null && (property.GetValue(o)?.Equals(indexer.Identifier) ?? false);
                  });

               if (element is null)
               {
                  var elementType = typeof(object);
                  var collectionType = currentInstance.GetType();
                  if (collectionType.IsGenericType)
                  {
                     elementType = collectionType.GetGenericArguments()[0];
                  }

                  element = CreateInstanceOf(elementType)!;
               }

               if (currentInstance is IList collection)
               {
                  collection.Add(element);
               }

               currentInstance = element;
               currentType = element.GetType();
            }
               break;
         }
      }

      return currentInstance!;
   }

   private static object CreateInstanceOf(Type type)
   {
      if (type == typeof(string))
      {
         return string.Empty;
      }

      if (type == typeof(Guid))
      {
         return Guid.Empty;
      }

      if (!type.IsPrimitive)
      {
         var arguments = new List<object>();
         if (type.IsAssignableTo(typeof(IEnumerable)) && type != typeof(string))
         {
            if (type.IsGenericType)
            {
               var list = typeof(List<>).MakeGenericType(type.GetGenericArguments());
               return Activator.CreateInstance(list)!;
            }

            return Array.Empty<object>();
         }

         var constructor = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault();
         if (constructor is not null)
         {
            arguments.AddRange(constructor.GetParameters().Select(
               parameterInfo => CreateInstanceOf(parameterInfo.ParameterType)));
            return constructor.Invoke(arguments.ToArray());
         }
      }

      return Activator.CreateInstance(type)!;
   }
}