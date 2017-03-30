using System;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using Android.Widget;
using JetBrains.Annotations;

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
         menuListView.Adapter = new ArrayAdapter<string>(this, Resource.Layout.MainMenuItem, menuItems);
         menuListView.OnItemClickListener = new ItemClickListenerImlp(Resources, this);
      }

      private sealed class ItemClickListenerImlp : AdapterView.IOnItemClickListener
      {
         private readonly Activity _ownerActivity;
         private readonly Resources _resources;

         public ItemClickListenerImlp(Resources resources, Activity ownerActivity)
         {
            _resources = resources;
            _ownerActivity = ownerActivity;
         }

         public void Dispose()
         {
         }

         [UsedImplicitly]
         public IntPtr Handle { get; }

         public void OnItemClick(AdapterView parent, View view, int position, long id)
         {
            var clickedTextView = view as TextView;
            if (clickedTextView != null)
            {
               var text = clickedTextView.Text;
               var intent = GetCurrentIntent(text);
               if (intent != null)
                  _ownerActivity.StartActivity(intent);
            }
         }

         private Intent GetCurrentIntent(string text)
         {
            Intent intent = null;
            if (text.Equals(_resources.GetString(Resource.String.MenuItemStart),
               StringComparison.CurrentCultureIgnoreCase))
               intent = new Intent(_ownerActivity, typeof(StartScreenActivity));
            else if (text.Equals(_resources.GetString(Resource.String.MenuItemSettings),
               StringComparison.CurrentCultureIgnoreCase))
               intent = new Intent(_ownerActivity, typeof(SettingsScreenActivity));
            else if (text.Equals(_resources.GetString(Resource.String.MenuItemAbout),
               StringComparison.CurrentCultureIgnoreCase))
               intent = new Intent(_ownerActivity, typeof(AboutScreenActivity));

            return intent;
         }
      }
   }
}