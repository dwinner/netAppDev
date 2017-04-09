using System.IO;
using System.Text;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace QuizApp
{
   [Activity(Label = "@string/help")]
   public class QuizHelpActivity : Activity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.Help);
         var helpTextView = FindViewById<TextView>(Resource.Id.TextView_HelpText);

         // Читаем файл в строку и заполняем TextView
         try
         {
            using (var fIn = Resources.OpenRawResource(Resource.Raw.QuizHelp))
            using (var reader = new StreamReader(fIn, Encoding.Default))
            {
               helpTextView.Text = reader.ReadToEnd();
            }
         }
         catch (IOException ioEx)
         {
            Log.Error(GetType().Name, ioEx.Message, ioEx);
         }
      }
   }
}