using Android.App;
using Android.Content;
using Android.Gms.Common;
using FlickrLocart_App.Abstract;
using Fragment = Android.Support.V4.App.Fragment;
using JObj = Java.Lang.Object;

namespace FlickrLocart_App
{
   public class LocatrActivity : SingleFragmentActivity
   {
      private const int RequestError = 0;

      protected override Fragment CreateFragment() => LocatrFragment.Instance;

      protected override void OnResume()
      {
         base.OnResume();

         var apiAvailability = GoogleApiAvailability.Instance;
         var errorCode = apiAvailability.IsGooglePlayServicesAvailable(this);
         if (errorCode != ConnectionResult.Success)
         {
            IDialogInterfaceOnCancelListener dialogIfaceImpl = new CancelHandlerImpl(this);
            var errorDialog = apiAvailability.GetErrorDialog(this, errorCode, RequestError, dialogIfaceImpl);
            errorDialog.Show();
         }
      }

      private sealed class CancelHandlerImpl : JObj, IDialogInterfaceOnCancelListener
      {
         private readonly Activity _activity;

         public CancelHandlerImpl(Activity activity) => _activity = activity;

         public void OnCancel(IDialogInterface dialog) => _activity.Finish();
      }
   }
}