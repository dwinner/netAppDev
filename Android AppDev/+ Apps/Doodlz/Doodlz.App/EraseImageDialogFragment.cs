/**
 * Allows user to erase image
 */

using Android.App;
using Android.Content;
using Android.OS;
using DialogFragmentV4 = Android.Support.V4.App.DialogFragment;
using StringRes = Doodlz.App.Resource.String;
using IdRes = Doodlz.App.Resource.Id;

namespace Doodlz.App
{
   /// <summary>
   ///    Class for the Erase Image dialog
   /// </summary>
   public class EraseImageDialogFragment : DialogFragmentV4
   {
      /// <summary>
      ///    Gets a reference to the MainActivityFragment
      /// </summary>
      private MainActivityFragment DoodleFragment =>
         (MainActivityFragment) FragmentManager.FindFragmentById(IdRes.doodleFragment);

      public override Dialog OnCreateDialog(Bundle savedInstanceState) // Create an AlertDialog and return it
         => new AlertDialog.Builder(Activity)
            .SetMessage(StringRes.message_erase)
            .SetPositiveButton(StringRes.button_erase, (sender, args) => DoodleFragment.DoodleView.Clear())
            .SetNegativeButton(Android.Resource.String.Cancel, (sender, args) => { })
            .Create();

      public override void OnAttach(Context context) // Tell MainActivityFragment that dialog is now displayed
      {
         base.OnAttach(context);
         if (DoodleFragment != null) DoodleFragment.DialogOnScreen = true;
      }

      public override void OnDetach() // Tell MainActivityFragment that dialog is no longer displayed
      {
         base.OnDetach();
         if (DoodleFragment != null) DoodleFragment.DialogOnScreen = false;
      }
   }
}