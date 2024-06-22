namespace NullObject;

internal class OptionalLog(ILog impl) : ILog
{
   public static ILog NoLogging = null!;

   public void Info(string msg)
   {
      impl?.Info(msg);
   }

   public void Warn(string msg)
   {
      throw new NotImplementedException();
   }
}