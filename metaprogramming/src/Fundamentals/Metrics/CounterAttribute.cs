namespace Fundamentals.Metrics;

[AttributeUsage(AttributeTargets.Method)]
public sealed class CounterAttribute<T>(string name, string description) : Attribute
{
   public string Name { get; } = name;

   public string Description { get; } = description;
}