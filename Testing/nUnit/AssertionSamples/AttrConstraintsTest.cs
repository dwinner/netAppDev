namespace AssertionSamples;

[TestFixture]
public class AttrConstraintsTest
{
   [Test]
   public void Attr_Test()
   {
      Assert.That(typeof(Person),
         Has.Attribute<BacklogAttribute>().Property(nameof(BacklogAttribute.Author)).EqualTo("John Doe"));
      Assert.That(typeof(Person), Has.Attribute<BacklogAttribute>());
   }
}

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public sealed class BacklogAttribute(string author) : Attribute
{
   public string Author { get; init; } = author;
}

[Backlog("John Doe")]
public record Person(string FirstName, string SecondName);