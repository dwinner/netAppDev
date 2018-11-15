using Android.App;
using Android.OS;
using DialogFragment = Android.Support.V4.App.DialogFragment;

namespace PointOfViewApp
{
   public class DeleteDialogFragment : DialogFragment
   {
      public override Dialog OnCreateDialog(Bundle savedInstanceState)
      {
         var targetFragment = (PoiDetailFragment) TargetFragment;
         var poiName = Arguments.GetString("name");
         var dialog = new AlertDialog.Builder(Activity)
            .SetTitle("Confirm delete")
            .SetCancelable(false)
            .SetPositiveButton("OK", (sender, args) => targetFragment.DeletePoiAsync())
            .SetNegativeButton("Cancel", (sender, args) => { })
            .SetMessage($"Are you sure you want to delete {poiName}?")
            .Create();
         return dialog;
      }
   }
}