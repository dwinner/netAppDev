namespace DynamicAssemblyMp;

public class Employee
{
   [NotifyChangesFor(nameof(FullName))]
   public virtual string FirstName { get; set; } = string.Empty;

   [NotifyChangesFor(nameof(FullName))]
   public virtual string LastName { get; set; } = string.Empty;

   public virtual string FullName => $"{FirstName} {LastName}";
}