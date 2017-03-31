using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using static System.StringComparison;

namespace SatelliteMovingApp
{
   /// <summary>
   ///    Активность для экрана главного меню
   /// </summary>
   [Activity(Label = nameof(MenuScreenActivity))]
   public class MenuScreenActivity : Activity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         SetContentView(Resource.Layout.MenuScreen);
         SetupMenuListView();
      }

      private void SetupMenuListView()
      {
         var menuListView = FindViewById<ListView>(Resource.Id.MenuListViewId);
         string[] menuItems =
         {
            Resources.GetString(Resource.String.MenuItemStart),
            Resources.GetString(Resource.String.MenuItemSettings),
            Resources.GetString(Resource.String.MenuItemAbout)
         };
         View view = FindViewById(Resource.Layout.MainMenuItem);
         var textView = FindViewById<TextView>(Resource.Id.MainMenuTextView);
         menuListView.Adapter = new ArrayAdapter<string>(this, Resource.Layout.MainMenuItem, menuItems);
         menuListView.ItemClick += (sender, e) =>
         {
            var clickedTextView = e.View as TextView;
            if (clickedTextView != null)
            {
               var text = clickedTextView.Text;
               var intent = GetCurrentIntent(text);
               if (intent != null)
                  StartActivity(intent);
            }
         };
      }

      private Intent GetCurrentIntent(string text)
      {
         Intent intent = null;
         if (text.Equals(Resources.GetString(Resource.String.MenuItemStart), CurrentCultureIgnoreCase))
            intent = new Intent(this, typeof(StartScreenActivity));
         else if (text.Equals(Resources.GetString(Resource.String.MenuItemSettings), CurrentCultureIgnoreCase))
            intent = new Intent(this, typeof(SettingsScreenActivity));
         else if (text.Equals(Resources.GetString(Resource.String.MenuItemAbout), CurrentCultureIgnoreCase))
            intent = new Intent(this, typeof(AboutScreenActivity));

         return intent;
      }
   }
}