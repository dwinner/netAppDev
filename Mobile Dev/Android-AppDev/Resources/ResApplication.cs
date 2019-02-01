using Android.App;
using Android.Content.Res;
using Android.Util;

namespace Resources
{
   public class ResApplication : Application
   {
      private const string Tag = nameof(ResApplication);

      public override void OnConfigurationChanged(Configuration newConfig)
      {
         base.OnConfigurationChanged(newConfig);
         Log.Debug(Tag, "Configuration changed");
      }

      public override void OnCreate()
      {
         base.OnCreate();
         Log.Debug(Tag, nameof(OnCreate));
      }

      public override void OnLowMemory()
      {
         base.OnLowMemory();
         Log.Debug(Tag, nameof(OnLowMemory));
      }

      public override void OnTerminate()
      {
         base.OnTerminate();
         Log.Debug(Tag, nameof(OnTerminate));
      }
   }
}