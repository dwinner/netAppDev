using Microsoft.EntityFrameworkCore;
using Moq;

namespace EF;

public class UserControllerTestsBad
{
   private const string ConnectionString = @"Server=.\Sql;Database=IntegrationTests;Trusted_Connection=true;";

   [Fact]
   public void Changing_email_from_corporate_to_non_corporate()
   {
      var optionsBuilder = new DbContextOptionsBuilder<CrmContext>()
         /*.UseSqlServer(ConnectionString)*/;

      using (var context = new CrmContext(optionsBuilder.Options))
      {
         // Arrange
         var userRepository = new UserRepository(context);
         var companyRepository = new CompanyRepository(context);
         var user = new User(0, "user@mycorp.com",
            UserType.Employee, false);
         userRepository.SaveUser(user);
         var company = new Company("mycorp.com", 1);
         companyRepository.SaveCompany(company);
         context.SaveChanges();

         var busSpy = new BusSpy();
         var messageBus = new MessageBus(busSpy);
         var loggerMock = new Mock<IDomainLogger>();
         var sut = new UserController(
            context, messageBus, loggerMock.Object);

         // Act
         var result = sut.ChangeEmail(user.UserId, "new@gmail.com");

         // Assert
         Assert.Equal("OK", result);

         var userFromDb = userRepository.GetUserById(user.UserId);
         Assert.Equal("new@gmail.com", userFromDb.Email);
         Assert.Equal(UserType.Customer, userFromDb.Type);

         var companyFromDb = companyRepository.GetCompany();
         Assert.Equal(0, companyFromDb.NumberOfEmployees);

         busSpy.ShouldSendNumberOfMessages(1)
            .WithEmailChangedMessage(user.UserId, "new@gmail.com");
         loggerMock.Verify(
            x => x.UserTypeHasChanged(
               user.UserId, UserType.Employee, UserType.Customer),
            Times.Once);
      }
   }
}