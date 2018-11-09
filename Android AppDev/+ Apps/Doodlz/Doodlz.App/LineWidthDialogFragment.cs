/**
 * Allows user to set the drawing color on the DoodleView
 */

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using DialogFragmentV4 = Android.Support.V4.App.DialogFragment;
using IdRes = Doodlz.App.Resource.Id;
using LayoutRes = Doodlz.App.Resource.Layout;
using StringRes = Doodlz.App.Resource.String;

namespace Doodlz.App
{
   /// <summary>
   ///    Class for the Select Line Width dialog
   /// </summary>
   public class LineWidthDialogFragment : DialogFragmentV4
   {
      private ImageView _widthImageView;

      /// <summary>
      ///    Return a reference to the MainActivityFragment
      /// </summary>
      private MainActivityFragment DoodleFragment =>
         (MainActivityFragment) FragmentManager.FindFragmentById(IdRes.doodleFragment);

      public override Dialog OnCreateDialog(Bundle savedInstanceState) // Create an Alert dialog and return it
      {
         // Create the dialog
         var builder = new AlertDialog.Builder(Activity);
         var lineWidthDialogView = Activity.LayoutInflater.Inflate(LayoutRes.fragment_line_width, null);
         builder.SetView(lineWidthDialogView); // Add GUI to dialog
         builder.SetTitle(StringRes.title_line_width_dialog); // Set the AlertDialog's message
         _widthImageView = lineWidthDialogView.FindViewById<ImageView>(IdRes.widthImageView); // Get the ImageView

         // Configure widthSeekBar
         var doodleView = DoodleFragment.DoodleView;
         var widthSeekBar = lineWidthDialogView.FindViewById<SeekBar>(IdRes.widthSeekBar);
         widthSeekBar.ProgressChanged += OnSeekBarProgressChanged;
         widthSeekBar.Progress = doodleView.LineWidth;

         // Add set line width button
         builder.SetPositiveButton(StringRes.button_set_line_width,
            (sender, args) => doodleView.LineWidth = widthSeekBar.Progress);

         return builder.Create(); // Return dialog
      }

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

      private void OnSeekBarProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
      {
         var bitmap = Bitmap.CreateBitmap(400, 100, Bitmap.Config.Argb8888);
         var canvas = new Canvas(bitmap); // Draws into bitmap

         // Configure a Paint object for the current SeekBar value
         var paint = new Paint
         {
            Color = DoodleFragment.DoodleView.DrawingColor,
            StrokeCap = Paint.Cap.Round,
            StrokeWidth = e.Progress
         };

         // Erase the bitmap and redraw the line
         bitmap.EraseColor(Resources.GetColor(Android.Resource.Color.Transparent, Context.Theme));
         canvas.DrawLine(30, 50, 370, 50, paint);
         _widthImageView.SetImageBitmap(bitmap);
      }
   }
}