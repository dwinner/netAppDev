namespace Fundamentals;

public static class ConceptExtensions
{
   public static bool IsPiiConcept(this Type objectType) => objectType.IsDerivedFromOpenGeneric(typeof(PiiConceptAs<>));

   public static bool IsConcept(this Type objectType) => objectType.IsDerivedFromOpenGeneric(typeof(ConceptAs<>));

   public static bool IsConcept(this object instance) => IsConcept(instance.GetType());

   public static Type GetConceptValueType(this Type type) => ConceptMap.GetConceptValueType(type);

   public static object GetConceptValue(this object conceptObject)
   {
      if (!IsConcept(conceptObject))
      {
         throw new TypeIsNotAConceptException(conceptObject.GetType());
      }

      return ((dynamic)conceptObject).Value;
   }
}