namespace PassingArgumentsToMethods.ADummy;

public class CustomerService(ISendEmail emailSender)
{
   public Result SendEmail(string from, string to)
   {
      var result = new Result();

      if (string.IsNullOrEmpty(to))
      {
         result.ErrorMessages.Add("Cannot send an email with an empty to address");
         return result;
      }

      emailSender.SendEmail(from, to);

      return result;
   }
}