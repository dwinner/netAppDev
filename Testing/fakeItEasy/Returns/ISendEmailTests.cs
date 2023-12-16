using FakeItEasy;

namespace Returns;

[TestFixture]
public class SendEmailTests
{
   [SetUp]
   public void Given()
   {
      var emailSender = A.Fake<ISendEmail>();
      A.CallTo(() => emailSender.GetEmailServerAddress())
         .Returns("SMTPServerAddress");
      A.CallTo(() => emailSender.GetEmailServerAddress())
         .Returns(SmtpServerAddress);
      A.CallTo(() => emailSender.GetAllCcRecipients())
         .Returns(new List<string>
         {
            "CcRecipient1@somewhere.com",
            "CcRecipient2@somewhere.com"
         });
   }

   private const string SmtpServerAddress = "SmtpServerAddress";
}