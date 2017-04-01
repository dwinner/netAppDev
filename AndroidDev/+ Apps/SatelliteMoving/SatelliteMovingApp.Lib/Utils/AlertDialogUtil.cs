using Android.App;
using Android.Content;

namespace SatelliteMovingApp.Lib.Utils
{
   public static class AlertDialogUtil
   {
      public static void ShowSimpleDialog(Activity anOwnerActivity, string aMessage)
      {
         var errorDialog = new AlertDialog.Builder(anOwnerActivity).SetMessage(aMessage).Create();
         errorDialog.SetButton((int) DialogButtonType.Positive, "Ok", (sender, args) => { });
         errorDialog.Show();
      }
   }
}