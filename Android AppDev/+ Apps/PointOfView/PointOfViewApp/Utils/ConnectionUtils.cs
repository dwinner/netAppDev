using Android.Content;
using Android.Net;

namespace PointOfViewApp.Utils
{
   public static class ConnectionUtils
   {
      public static bool IsConnected(Context context)
      {
         var connectivityManager = (ConnectivityManager) context.GetSystemService(Context.ConnectivityService);
         var activeConnection = connectivityManager.ActiveNetworkInfo;
         return activeConnection?.IsConnected == true;
      }
   }
}