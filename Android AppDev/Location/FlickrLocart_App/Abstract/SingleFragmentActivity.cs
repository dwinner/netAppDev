using Android.OS;
using Android.Support.V7.App;
using FragmentV4 = Android.Support.V4.App.Fragment;
using ResId = FlickrLocart_App.Resource.Id;
using ResLayout = FlickrLocart_App.Resource.Layout;

namespace FlickrLocart_App.Abstract
{
   public abstract class SingleFragmentActivity : AppCompatActivity
   {
      protected abstract FragmentV4 CreateFragment();

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         SetContentView(ResLayout.ActivityFragment);
         var fragment = SupportFragmentManager.FindFragmentById(ResId.fragment_container);
         if (fragment == null)
         {
            fragment = CreateFragment();
            SupportFragmentManager.BeginTransaction()
               .Add(ResId.fragment_container, fragment)
               .Commit();
         }
      }
   }
}