using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Environment = System.Environment;

namespace Resources.Interfaces
{
   public abstract class MonitoredDebugActivity : MonitoredActivity, IReportBack
   {
      private const string DebugViewTextStateKey = "debugViewText";
      private static string _tag;
      private readonly int _menuId;
      private bool _retainState;

      protected MonitoredDebugActivity(string tag, int menuId)
         : base(tag)
      {
         _tag = tag;
         _menuId = menuId;
      }

      public void ReportBack(string aTag, string aMessage)
      {
         AppendText($"{_tag}:{aMessage}");
         Log.Debug(_tag, aMessage);
      }

      public void RetainState() => _retainState = true;

      public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
      {
         base.OnCreate(savedInstanceState, persistentState);
         SetContentView(Resource.Layout.debug_activity_layout);
      }

      public override bool OnCreateOptionsMenu(IMenu menu)
      {
         base.OnCreateOptionsMenu(menu);
         MenuInflater.Inflate(_menuId, menu);
         return true;
      }

      public override bool OnOptionsItemSelected(IMenuItem item)
      {
         AppendMenuItemText(item);
         if (item.ItemId == Resource.Id.menu_da_clear)
         {
            EmptyText();
            return true;
         }

         return base.OnOptionsItemSelected(item);
      }

      protected TextView GetTextView() => FindViewById<TextView>(Resource.Id.text1);

      private void EmptyText()
      {
         var tv = GetTextView();
         tv.Text = string.Empty;
      }

      private void AppendMenuItemText(IMenuItem item)
      {
         var title = item.TitleFormatted.ToString();
         var textView = GetTextView();
         textView.Text = $"{textView.Text}{Environment.NewLine}{title}";
      }

      protected abstract bool OnMenuItemSelected(IMenuItem item);

      private void AppendText(string aText)
      {
         var tv = GetTextView();
         tv.Text = $"{tv.Text}{Environment.NewLine}{aText}";
         Log.Debug(_tag, aText);
      }

      protected override void OnRestoreInstanceState(Bundle savedInstanceState)
      {
         var st = savedInstanceState.GetString(DebugViewTextStateKey);
         if (st == null) return;

         var tv = GetTextView();
         tv.Text = st;
         Log.Debug(_tag, "Restored state");
      }

      protected override void OnSaveInstanceState(Bundle outState)
      {
         base.OnSaveInstanceState(outState);
         if (!_retainState) return;

         // save state
         var tv = GetTextView();
         var t = tv.Text;
         outState.PutString(DebugViewTextStateKey, t);
         Log.Debug(_tag, "Saved state");
      }
   }
}