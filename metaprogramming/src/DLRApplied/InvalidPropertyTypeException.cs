namespace DLRApplied;

public class InvalidPropertyTypeException(string type, string property)
   : Exception($"Property '{property}' on '{type}' is invalid.");