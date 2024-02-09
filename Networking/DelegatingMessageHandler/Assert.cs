namespace DelegatingMessageHandler;

internal static class Assert
{
   public static void AreEqual(object o1, object o2)
   {
      if (!Equals(o1, o2))
      {
         throw new Exception("Objects are not equal");
      }
   }
}