using FakeItEasy;

namespace Invokes;

public static class CustomerServiceTests
{
   [TestFixture]
   public class WhenGettingCustomersLastAndFirstNamesAsCsv
   {
      [SetUp]
      public void Given()
      {
         var buildCsv = A.Fake<IBuildCsv>();
         A.CallTo(() => buildCsv.SetHeader(A<IEnumerable<string>>.Ignored))
            .Invokes(x => _appendedHeaders = (string[])x.Arguments.First());
         A.CallTo(() => buildCsv.AddRow(A<IEnumerable<string>>.Ignored))
            .Invokes(x => _appendedRows = (string[])x.Arguments.First());

         var customers = new List<Customer> { new() { LastName = "Doe", FirstName = "Jon" } };
         _bodyList = new[] { customers[0].LastName, customers[0].FirstName };

         var sut = new CustomerService(buildCsv);
         sut.GetLastAndFirstNamesAsCsv(customers);
      }

      private readonly string[] _headerList = { "Last Name", "First Name" };
      private string[] _bodyList;
      private string[] _appendedHeaders;
      private string[] _appendedRows;

      [Test]
      public void SetsCorrectHeader()
      {
         Assert.That(_appendedHeaders.SequenceEqual(_headerList));
      }

      [Test]
      public void AddsCorrectRows()
      {
         Assert.That(_appendedRows.SequenceEqual(_bodyList));
      }
   }
}