using Moq;

namespace V1;

public class IntegrationTests
{
   private const string ConnectionString = @"Server=.\Sql;Database=IntegrationTests;Trusted_Connection=true;";

   [Fact]
   public void Changing_email_from_corporate_to_non_corporate()
   {
      // Arrange
      var db = new Database(ConnectionString);
      var user = CreateUser("user@mycorp.com", UserType.Employee, db);
      CreateCompany("mycorp.com", 1, db);

      var messageBusMock = new Mock<IMessageBus>();
      var loggerMock = new Mock<IDomainLogger>();
      var sut = new UserController(
         db, messageBusMock.Object, loggerMock.Object);

      // Act
      var result = sut.ChangeEmail(user.UserId, "new@gmail.com");

      // Assert
      Assert.Equal("OK", result);

      var userData = db.GetUserById(user.UserId);
      var userFromDb = UserFactory.Create(userData);
      Assert.Equal("new@gmail.com", userFromDb.Email);
      Assert.Equal(UserType.Customer, userFromDb.Type);

      var companyData = db.GetCompany();
      var companyFromDb = CompanyFactory.Create(companyData);
      Assert.Equal(0, companyFromDb.NumberOfEmployees);

      messageBusMock.Verify(
         x => x.SendEmailChangedMessage(user.UserId, "new@gmail.com"),
         Times.Once);
      loggerMock.Verify(
         x => x.UserTypeHasChanged(
            user.UserId, UserType.Employee, UserType.Customer),
         Times.Once);
   }

   private Company CreateCompany(string domainName, int numberOfEmployees, Database database)
   {
      var company = new Company(domainName, numberOfEmployees);
      database.SaveCompany(company);
      return company;
   }

   private User CreateUser(string email, UserType type, Database database)
   {
      var user = new User(0, email, type, false);
      database.SaveUser(user);
      return user;
   }
}