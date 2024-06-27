namespace DynamicAssemblyMp;

[AttributeUsage(AttributeTargets.Property)]
public class NotifyChangesForAttribute(params string[] propertyNames) : Attribute
{
   public string[] PropertyNames { get; } = propertyNames;
}