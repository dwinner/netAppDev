// Внедрение в метод через postsharp-атрибуты

namespace TweetInterception.ViaPs
{
   internal static class Program
   {
      private static void Main()
      {
         var svc = new TwitterClient();
         svc.Send("Hi");
      }
   }
}