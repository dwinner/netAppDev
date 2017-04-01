using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Open_GL_ES_AppSample
{
   [Activity(Label = "Open_GL_ES_AppSample",
      MainLauncher = true,
      Icon = "@drawable/icon",
      ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden
#if __ANDROID_11__
		,HardwareAccelerated=false
#endif
   )]
   public class MainActivity : Activity
   {
      private GlView1 _view;

      protected override void OnCreate(Bundle bundle)
      {
         base.OnCreate(bundle);

         // Create our OpenGL view, and display it
         _view = new GlView1(this);
         SetContentView(_view);
      }

      protected override void OnPause()
      {
         base.OnPause();
         _view.Pause();
      }

      protected override void OnResume()
      {
         base.OnResume();
         _view.Resume();
      }
   }
}