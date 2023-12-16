namespace Returns;

public interface ISendEmail
{
   string GetEmailServerAddress();
   List<string> GetAllCcRecipients();
}