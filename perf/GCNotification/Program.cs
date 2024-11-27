using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GCNotification
{
   internal static class Program
   {
      private static void Main()
      {
         const int arrSize = 1024;
         var arrays = new List<byte[]>();

         GC.RegisterForFullGCNotification(25, 25);

         // Start a separate thread to wait for GC notifications
         Task.Run(() => WaitForGcThread(null));

         Console.WriteLine("Press any key to exit");
         while (!Console.KeyAvailable)
         {
            try
            {
               arrays.Add(new byte[arrSize]);
            }
            catch (OutOfMemoryException)
            {
               Console.WriteLine("OutOfMemoryException!");
               arrays.Clear();
            }
         }

         GC.CancelFullGCNotification();
      }

      private static void WaitForGcThread(object arg)
      {
         const int maxWaitMs = 10_000;
         while (true)
         {
            // There is also an overload of WaitForFullGCApproach that waits indefinitely
            var status = GC.WaitForFullGCApproach(maxWaitMs);
            var didCollect = false;
            switch (status)
            {
               case GCNotificationStatus.Succeeded:
                  Console.WriteLine("GC approaching!");
                  Console.WriteLine("-- redirect processing to another machine -- ");
                  didCollect = true;
                  GC.Collect();
                  break;
               case GCNotificationStatus.Canceled:
                  Console.WriteLine("GC Notification was canceled");
                  break;
               case GCNotificationStatus.Timeout:
                  Console.WriteLine("GC notification timed out");
                  break;
            }

            if (didCollect)
            {
               do
               {
                  status = GC.WaitForFullGCComplete(maxWaitMs);
                  switch (status)
                  {
                     case GCNotificationStatus.Succeeded:
                        Console.WriteLine("GC completed");
                        Console.WriteLine("-- accept processing on this machine again --");
                        break;
                     case GCNotificationStatus.Canceled:
                        Console.WriteLine("GC Notification was canceled");
                        break;
                     case GCNotificationStatus.Timeout:
                        Console.WriteLine("GC completion notification timed out");
                        break;
                  }
                  // Looping isn't necessary, but it's useful if you want
                  // to check other state before waiting again.
               } while (status == GCNotificationStatus.Timeout);
            }
         }
      }
   }
}