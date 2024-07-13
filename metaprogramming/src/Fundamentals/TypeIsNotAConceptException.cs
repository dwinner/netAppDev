namespace Fundamentals;

public class TypeIsNotAConceptException(Type type)
   : Exception($"Type '{type.AssemblyQualifiedName}' is not a concept - implement ConceptAs<> for it to be one.");