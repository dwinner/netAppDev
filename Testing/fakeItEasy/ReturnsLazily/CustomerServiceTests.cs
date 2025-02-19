using FakeItEasy;

namespace ReturnsLazily;

[TestFixture]
public class WhenGettingCustomerNamesAsCsv
{
   [SetUp]
   public void Given()
   {
      var customers = new List<Customer>
      {
         new() { Id = 1, FirstName = "FirstName1", LastName = "LastName1" },
         new() { Id = 2, FirstName = "FirstName2", LastName = "LastName2" }
      };

      var employeeRepository = A.Fake<ICustomerRepository>();
      A.CallTo(() => employeeRepository.GetCustomerById(A<int>.Ignored))
         .ReturnsLazily<Customer, int>(id => customers.Single(customer => customer.Id == id));

      var sut = new CustomerService(employeeRepository);
      result = sut.GetCustomerNamesAsCsv(customers.Select(x => x.Id).ToArray());
   }

   private string result;

   [Test]
   public void ReturnsCustomerNamesAsCsv()
   {
      Assert.That(result, Is.EqualTo("FirstName1 LastName1,FirstName2 LastName2"));
   }
}