using Android.App;
using Android.OS;
using DialogFragment = Android.Support.V4.App.DialogFragment;

namespace PointOfViewApp
{
   public class ProgressDialogFragment : DialogFragment
   {
      public override Dialog OnCreateDialog(Bundle savedInstanceState)
      {
         Cancelable = false;
         // TODO: Use ProgressDialogFragment instead
#pragma warning disable 618
         var progressDialog = new ProgressDialog(Activity) {Indeterminate = true};
#pragma warning restore 618
         progressDialog.SetMessage("Getting location...");
         progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);

         return progressDialog;
      }
   }
}