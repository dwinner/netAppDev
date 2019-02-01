using System;

namespace TweetInterception.ViaPs
{
   public class TwitterClient
   {
      [MyInterceptorAspect]
      public void Send(string aMessage)
      {
         Console.WriteLine("Sending: {0}", aMessage);
      } 
   }
}