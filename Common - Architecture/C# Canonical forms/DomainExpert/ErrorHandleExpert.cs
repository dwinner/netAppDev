namespace DomainExpert
{
   public class ErrorHandleExpert : IExpert
   {
      private readonly string _errorMessage;

      public ErrorHandleExpert(string errorMessage)
      {
         _errorMessage = errorMessage;
      }

      public void Handle(ILog aLog)
      {
         aLog.Report(_errorMessage);
      }
   }
}