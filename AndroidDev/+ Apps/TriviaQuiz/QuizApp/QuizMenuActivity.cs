using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace QuizApp
{
   [Activity(Label = "@string/menu")]
   public class QuizMenuActivity : Activity
   {
      private Dictionary<string, Intent> _menuIntentMap;

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         _menuIntentMap = new Dictionary<string, Intent>
         {
            [Resources.GetString(Resource.String.menu_item_play)] = new Intent(this, typeof(QuizGameActivity)),
            [Resources.GetString(Resource.String.menu_item_scores)] = new Intent(this, typeof(QuizScoresActivity)),
            [Resources.GetString(Resource.String.menu_item_settings)] = new Intent(this, typeof(QuizSettingsActivity)),
            [Resources.GetString(Resource.String.menu_item_help)] = new Intent(this, typeof(QuizHelpActivity))
         };
         SetContentView(Resource.Layout.Menu);
         var menuList = FindViewById<ListView>(Resource.Id.ListView_Menu);
         var adapter = new ArrayAdapter<string>(this, Resource.Layout.MenuItem, _menuIntentMap.Keys.ToList());
         menuList.Adapter = adapter;
         menuList.ItemClick += (sender, args) =>
         {
            if (args.View is TextView clickedTextView)
            {
               var clickedText = clickedTextView.Text;
               var intentToCall = GetIntentToCall(clickedText);
               if (intentToCall != null)
                  StartActivity(intentToCall);
            }
         };
      }

      private Intent GetIntentToCall(string clickedText)
         => (from intentPair in _menuIntentMap
            where intentPair.Key.Equals(clickedText, StringComparison.CurrentCultureIgnoreCase)
            select intentPair.Value).FirstOrDefault();
   }
}