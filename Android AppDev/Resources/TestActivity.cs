using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Resources.Interfaces;
using ImageRes = Resources.Resource.Drawable;
using IdRes = Resources.Resource.Id;

namespace Resources
{
   [Activity(Label = "Test Resources", MainLauncher = true)]
   public class TestActivity : MonitoredDebugActivity
   {
      private const string Tag = nameof(TestActivity);
      private ResourceTester _resourceTester;

      public TestActivity() : base(Tag, Resource.Menu.main_menu)
      {
      }

      public TestActivity(string tag, int menuId) : base(tag, menuId)
      {
      }

      public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
      {
         base.OnCreate(savedInstanceState, persistentState);
         _resourceTester = new ResourceTester(this, this);
      }

      protected override bool OnMenuItemSelected(IMenuItem item)
      {
         Log.Debug(Tag, item.TitleFormatted.ToString());

         switch (item.ItemId)
         {
            case IdRes.menu_test_strings:
               _resourceTester.TestEnStrings();
               break;

            case IdRes.menu_test_arrays:
               _resourceTester.TestStringArray();
               break;

            case IdRes.menu_test_layout:
               var intent = new Intent(this, typeof(HelloWorldActivity));
               StartActivity(intent);
               break;

            case IdRes.menu_test_color_drawables:
               TestColorDrawable();
               break;

            case IdRes.menu_test_colors:
               _resourceTester.TestColors();
               break;

            case IdRes.menu_test_image:
               TestImage();
               break;

            case IdRes.menu_test_shape:
               TestShape();
               break;

            case IdRes.menu_test_string_variations:
               _resourceTester.TestStringVariations();
               break;

            case IdRes.menu_test_xml:
               _resourceTester.TestXml();
               break;

            case IdRes.menu_test_rawfile:
               _resourceTester.TestRawFile();
               break;

            case IdRes.menu_test_assets:
               _resourceTester.TestAssets();
               break;
         }

         return true;
      }

      private void TestImage() => GetTextView().SetBackgroundResource(ImageRes.sample_image);

      private void TestColorDrawable() => GetTextView().SetBackgroundResource(ImageRes.red_rectangle);

      private void TestShape() => GetTextView().SetBackgroundResource(ImageRes.my_rounded_rectangle);
   }
}