using Android.App;
using Android.Content.Res;
using Android.OS;
using Android.Util;
using Java.Lang;

namespace Resources
{
   public class MonitoredActivity : Activity
   {
      private readonly string _tag;

      public MonitoredActivity(string tag) => _tag = tag;

      public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
      {
         Debug(nameof(OnCreate));
         base.OnCreate(savedInstanceState, persistentState);
      }

      protected override void OnPause()
      {
         Debug($"{nameof(OnPause)}: I may be partially or fully invisible");
         base.OnPause();
      }

      protected override void OnStop()
      {
         Debug($"{nameof(OnStop)}: I am fully invisible");
         base.OnStop();
      }

      protected override void OnDestroy()
      {
         Debug($"{nameof(OnDestroy)}: About to be removed");
         base.OnDestroy();
      }

      protected override void OnRestart()
      {
         Debug($"{nameof(OnRestart)}: UI controls are there");
         base.OnRestart();
      }

      protected override void OnStart()
      {
         Debug($"{nameof(OnStart)}: UI may be partially visible");
         base.OnStart();
      }

      protected override void OnResume()
      {
         Debug($"{nameof(OnResume)}: UI fully visible");
         base.OnResume();
      }

      protected override void OnRestoreInstanceState(Bundle savedInstanceState)
      {
         Debug($"{nameof(OnRestoreInstanceState)}: You should restore the state");
         base.OnRestoreInstanceState(savedInstanceState);
      }

      public override Object OnRetainNonConfigurationInstance()
      {
         Debug(nameof(OnRetainNonConfigurationInstance));
         return base.OnRetainNonConfigurationInstance();
      }

      protected override void OnSaveInstanceState(Bundle outState)
      {
         Debug($"{nameof(OnSaveInstanceState)}: You should load up the bundle");
         base.OnSaveInstanceState(outState);
      }

      public override void OnConfigurationChanged(Configuration newConfig)
      {
         Debug(nameof(OnConfigurationChanged));
         base.OnConfigurationChanged(newConfig);
      }

      private void Debug(string aDebugMessage) => Log.Debug(_tag, aDebugMessage);
   }
}