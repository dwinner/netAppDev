using Moq;

namespace Transaction;

public class UserControllerTests
{
   private const string ConnectionString = @"Server=.\Sql;Database=IntegrationTests;Trusted_Connection=true;";

   [Fact]
   public void Changing_email_from_corporate_to_non_corporate()
   {
      // Arrange
      User user;
      using (var transaction = new Transaction(ConnectionString))
      {
         var userRepository = new UserRepository(transaction);
         var companyRepository = new CompanyRepository(transaction);
         user = CreateUser(
            "user@mycorp.com", UserType.Employee, userRepository);
         CreateCompany("mycorp.com", 1, companyRepository);

         transaction.Commit();
      }

      var busSpy = new BusSpy();
      var messageBus = new MessageBus(busSpy);
      var loggerMock = new Mock<IDomainLogger>();

      string result;
      using (var transaction = new Transaction(ConnectionString))
      {
         var sut = new UserController(transaction, messageBus, loggerMock.Object);

         // Act
         result = sut.ChangeEmail(user.UserId, "new@gmail.com");
      }

      // Assert
      Assert.Equal("OK", result);

      using (var transaction = new Transaction(ConnectionString))
      {
         var userRepository = new UserRepository(transaction);
         var companyRepository = new CompanyRepository(transaction);

         var userData = userRepository.GetUserById(user.UserId);
         var userFromDb = UserFactory.Create(userData);
         Assert.Equal("new@gmail.com", userFromDb.Email);
         Assert.Equal(UserType.Customer, userFromDb.Type);

         var companyData = companyRepository.GetCompany();
         var companyFromDb = CompanyFactory.Create(companyData);
         Assert.Equal(0, companyFromDb.NumberOfEmployees);

         busSpy.ShouldSendNumberOfMessages(1)
            .WithEmailChangedMessage(user.UserId, "new@gmail.com");
         loggerMock.Verify(
            x => x.UserTypeHasChanged(
               user.UserId, UserType.Employee, UserType.Customer),
            Times.Once);
      }
   }

   private Company CreateCompany(string domainName, int numberOfEmployees, CompanyRepository repository)
   {
      var company = new Company(domainName, numberOfEmployees);
      repository.SaveCompany(company);
      return company;
   }

   private User CreateUser(string email, UserType type, UserRepository repository)
   {
      var user = new User(0, email, type, false);
      repository.SaveUser(user);
      return user;
   }
}