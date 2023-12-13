using FluentAssertions;

namespace DomainEvents;

public class Tests
{
   [Fact]
   public void Changing_email_from_corporate_to_non_corporate()
   {
      var company = new Company("mycorp.com", 1);
      var sut = new User(1, "user@mycorp.com", UserType.Employee, false);

      sut.ChangeEmail("new@gmail.com", company);

      company.NumberOfEmployees.Should().Be(0);
      sut.Email.Should().Be("new@gmail.com");
      sut.Type.Should().Be(UserType.Customer);
      sut.EmailChangedEvents.Should().Equal(
         new EmailChangedEvent(1, "new@gmail.com"));
   }
}