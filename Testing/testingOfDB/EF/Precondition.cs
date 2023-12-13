namespace EF;

public static class Precondition
{
   public static void Requires(bool precondition, string message = null)
   {
      if (precondition == false)
      {
         throw new Exception(message);
      }
   }
}