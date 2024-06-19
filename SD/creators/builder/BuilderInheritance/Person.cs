namespace BuilderInheritance;

public class Person
{
   public DateTime DateOfBirth { get; set; }

   public string Name { get; set; } = string.Empty;

   public string Position { get; set; } = string.Empty;

   public static Builder New => new();

   public override string ToString() => $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";

   public class Builder : PersonBirthDateBuilder<Builder>
   {
      internal Builder()
      {
      }
   }
}