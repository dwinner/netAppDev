using FluentAssertions;

namespace FunctionalBasedArc;

public class Tests
{
   [Fact]
   public void A_new_file_is_created_when_the_current_file_overflows()
   {
      var sut = new AuditManager(3);
      var files = new FileContent[]
      {
         new("audit_1.txt", Array.Empty<string>()),
         new("audit_2.txt", new[]
         {
            "Peter; 2019-04-06T16:30:00",
            "Jane; 2019-04-06T16:40:00",
            "Jack; 2019-04-06T17:00:00"
         })
      };

      var update = sut.AddRecord(
         files, "Alice", DateTime.Parse("2019-04-06T18:00:00"));

      Assert.Equal("audit_3.txt", update.FileName);
      Assert.Equal("Alice;2019-04-06T18:00:00", update.NewContent);
      Assert.Equal(
         new FileUpdate("audit_3.txt", "Alice;2019-04-06T18:00:00"),
         update);
      update.Should().Be(
         new FileUpdate("audit_3.txt", "Alice;2019-04-06T18:00:00"));
   }
}