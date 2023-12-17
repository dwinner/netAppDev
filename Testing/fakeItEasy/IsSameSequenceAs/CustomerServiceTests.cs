using FakeItEasy;

namespace IsSameSequenceAs;

[TestFixture]
public class WhenGettingCustomersLastAndFirstNamesAsCsv
{
   [SetUp]
   public void Given()
   {
      _buildCsv = A.Fake<IBuildCsv>();
      var sut = new CustomerService(_buildCsv);
      _customers = new List<Customer>
      {
         new() { LastName = "Doe", FirstName = "Jon" },
         new() { LastName = "McCarthy", FirstName = "Michael" }
      };

      sut.GetLastAndFirstNamesAsCsv(_customers);
   }

   private IBuildCsv _buildCsv = null!;
   private List<Customer> _customers = null!;

   [Test]
   public void SetsCorrectHeader()
   {
      A.CallTo(() => _buildCsv.SetHeader(A<IEnumerable<string>>.That.IsSameSequenceAs("Last Name", "First Name")))
         .MustHaveHappened(1, Times.Exactly);
   }

   [Test]
   public void AddsCorrectRows()
   {
      //A.CallTo(() => buildCsv.AddRow(A<IEnumerable<string>>.That.IsSameSequenceAs(new[] { customers[0].LastName, customers[0].FirstName }))).MustHaveHappened(Repeated.Exactly.Once);
      //A.CallTo(() => buildCsv.AddRow(A<IEnumerable<string>>.That.IsSameSequenceAs(new[] { customers[1].LastName, customers[1].FirstName }))).MustHaveHappened(Repeated.Exactly.Once);

      foreach (var customer in _customers)
      {
         A.CallTo(() =>
               _buildCsv.AddRow(A<IEnumerable<string>>.That.IsSameSequenceAs(customer.LastName, customer.FirstName)))
            .MustHaveHappened(1, Times.Exactly);
      }
   }

   [Test]
   public void AddRowsIsCalledForEachCustomer()
   {
      A.CallTo(() => _buildCsv.AddRow(A<IEnumerable<string>>.Ignored))
         .MustHaveHappened(_customers.Count, Times.Exactly);
   }

   [Test]
   public void CsvIsBuilt()
   {
      A.CallTo(() => _buildCsv.Build()).MustHaveHappened(1, Times.Exactly);
   }
}