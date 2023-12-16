using FakeItEasy;

namespace PassingArgumentsToMethods.ADummy;

[TestFixture]
public class WhenSendingAnEmailWithAnEmptyToAddress
{
   [SetUp]
   public void Given()
   {
      var sut = new CustomerService(A.Fake<ISendEmail>());
      result = sut.SendEmail(A.Dummy<string>(), string.Empty);
   }

   private Result result;

   [Test]
   public void ReturnsErrorMessage()
   {
      Assert.That(result.ErrorMessages.Single(), Is.EqualTo("Cannot send an email with an empty to address"));
   }
}