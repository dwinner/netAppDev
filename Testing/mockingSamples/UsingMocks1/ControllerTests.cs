using Moq;

namespace UsingMocks1;

public class ControllerTests
{
   [Fact]
   public void Sending_a_greetings_email()
   {
      var emailGatewayMock = new Mock<IEmailGateway>();
      var sut = new Controller(emailGatewayMock.Object);

      sut.GreetUser("user@email.com");

      emailGatewayMock.Verify(
         x => x.SendGreetingsEmail("user@email.com"),
         Times.Once);
   }

   [Fact]
   public void Creating_a_report()
   {
      var stub = new Mock<IDatabase>();
      stub.Setup(x => x.GetNumberOfUsers()).Returns(10);
      var sut = new Controller(stub.Object);

      var report = sut.CreateReport();

      Assert.Equal(10, report.NumberOfUsers);
   }
}