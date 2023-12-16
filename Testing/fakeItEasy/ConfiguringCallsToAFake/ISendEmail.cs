namespace ConfiguringCallsToAFake;

public interface ISendEmail
{
   bool BodyIsHtml { get; set; }
   string GetEmailServerAddress();
   void SendMail();
}