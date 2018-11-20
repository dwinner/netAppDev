using Android.Support.V7.App;
using FragmentV4 = Android.Support.V4.App.Fragment;

namespace FlickrLocart_App.Abstract
{
   public abstract class SingleFragmentActivity : AppCompatActivity
   {
      protected abstract FragmentV4 CreateFragment();
   }
}