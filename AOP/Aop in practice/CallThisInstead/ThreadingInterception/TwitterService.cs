using System;
using System.Threading;

namespace ThreadingInterception
{
   public class TwitterService
   {
      public string GetTweet()
      {
         Thread.Sleep(3000);
         return $"Tweet from {DateTime.Now.TimeOfDay}";
      }
   }
}