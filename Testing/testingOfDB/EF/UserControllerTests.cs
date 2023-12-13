using Moq;

namespace EF;

public class UserControllerTests : IntegrationTests
{
   [Fact]
   public void Changing_email_from_corporate_to_non_corporate()
   {
      // Arrange
      var user = CreateUser();
      CreateCompany("mycorp.com", 1);

      var busSpy = new BusSpy();
      var messageBus = new MessageBus(busSpy);
      var loggerMock = new Mock<IDomainLogger>();

      // Act
      var result = Execute(
         x => x.ChangeEmail(user.UserId, "new@gmail.com"),
         messageBus, loggerMock.Object);

      // Assert
      Assert.Equal("OK", result);

      var userFromDb = QueryUser(user.UserId);
      userFromDb
         .ShouldExist()
         .WithEmail("new@gmail.com")
         .WithType(UserType.Customer);
      var companyFromDb = QueryCompany();
      Assert.Equal(0, companyFromDb.NumberOfEmployees);

      busSpy.ShouldSendNumberOfMessages(1)
         .WithEmailChangedMessage(user.UserId, "new@gmail.com");
      loggerMock.Verify(
         x => x.UserTypeHasChanged(
            user.UserId, UserType.Employee, UserType.Customer),
         Times.Once);
   }

   private string Execute(Func<UserController, string> func, MessageBus messageBus, IDomainLogger logger)
   {
      using (var context = new CrmContext(ConnectionString))
      {
         var controller = new UserController(context, messageBus, logger);
         return func(controller);
      }
   }

   private Company QueryCompany()
   {
      using (var context = new CrmContext(ConnectionString))
      {
         var repository = new CompanyRepository(context);
         return repository.GetCompany();
      }
   }

   private User QueryUser(int userId)
   {
      using (var context = new CrmContext(ConnectionString))
      {
         var repository = new UserRepository(context);
         return repository.GetUserById(userId);
      }
   }

   private User CreateUser(
      string email = "user@mycorp.com",
      UserType type = UserType.Employee,
      bool isEmailConfirmed = false)
   {
      using (var context = new CrmContext(ConnectionString))
      {
         var user = new User(0, email, type, isEmailConfirmed);
         var repository = new UserRepository(context);
         repository.SaveUser(user);

         context.SaveChanges();

         return user;
      }
   }

   private Company CreateCompany(string domainName, int numberOfEmployees)
   {
      using (var context = new CrmContext(ConnectionString))
      {
         var company = new Company(domainName, numberOfEmployees);
         var repository = new CompanyRepository(context);
         repository.AddCompany(company);

         context.SaveChanges();

         return company;
      }
   }
}