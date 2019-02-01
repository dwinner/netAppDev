using Android.App;
using Android.OS;
using Android.Widget;

namespace Resources
{
   public class HelloWorldActivity : Activity
   {
      public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
      {
         base.OnCreate(savedInstanceState, persistentState);
         SetContentView(Resource.Layout.main);
         var tv = FindViewById<TextView>(Resource.Id.text1);
         tv.Text = "Try this text instead";
      }
   }
}