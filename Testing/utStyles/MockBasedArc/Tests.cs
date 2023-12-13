using Moq;

namespace MockBasedArc;

public class Tests
{
   [Fact]
   public void A_new_file_is_created_for_the_first_entry()
   {
      var fileSystemMock = new Mock<IFileSystem>();
      fileSystemMock
         .Setup(x => x.GetFiles("audits"))
         .Returns(Array.Empty<string>());
      var sut = new AuditManager(3, "audits", fileSystemMock.Object);

      sut.AddRecord("Peter", DateTime.Parse("2019-04-09T13:00:00"));

      fileSystemMock.Verify(x => x.WriteAllText(
         @"audits\audit_1.txt",
         "Peter;2019-04-09T13:00:00"));
   }

   [Fact]
   public void A_new_file_is_created_when_the_current_file_overflows()
   {
      var fileSystemMock = new Mock<IFileSystem>();
      fileSystemMock
         .Setup(x => x.GetFiles("audits"))
         .Returns(new[]
         {
            @"audits\audit_1.txt",
            @"audits\audit_2.txt"
         });
      fileSystemMock
         .Setup(x => x.ReadAllLines(@"audits\audit_2.txt"))
         .Returns(new List<string>
         {
            "Peter; 2019-04-06T16:30:00",
            "Jane; 2019-04-06T16:40:00",
            "Jack; 2019-04-06T17:00:00"
         });
      var sut = new AuditManager(3, "audits", fileSystemMock.Object);

      sut.AddRecord("Alice", DateTime.Parse("2019-04-06T18:00:00"));

      fileSystemMock.Verify(x => x.WriteAllText(
         @"audits\audit_3.txt",
         "Alice;2019-04-06T18:00:00"));
   }
}