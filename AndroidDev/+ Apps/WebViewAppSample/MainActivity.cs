using System.Web;
using Android.App;
using Android.OS;
using Android.Webkit;
using WebViewAppSample.Models;
using WebViewAppSample.Views;

namespace WebViewAppSample
{
   [Activity(Label = "WebViewAppSample", MainLauncher = true)]
   public class MainActivity : Activity
   {
      protected override void OnCreate(Bundle bundle)
      {
         base.OnCreate(bundle);

         // Set our view from the "main" layout resource
         SetContentView(Resource.Layout.Main);

         var webView = FindViewById<WebView>(Resource.Id.webView);
         webView.Settings.JavaScriptEnabled = true;

         // Use subclassed WebViewClient to intercept hybrid native calls
         webView.SetWebViewClient(new HybridWebViewClient());

         // Render the view from the type generated from RazorView.cshtml
         var model = new Model1 {Text = "Text goes here"};
         var template = new RazorView {Model = model};
         var page = template.GenerateString();

         // Load the rendered HTML into the view with a base URL 
         // that points to the root of the bundled Assets folder
         webView.LoadDataWithBaseURL("file:///android_asset/", page, "text/html", "UTF-8", null);
      }

      private class HybridWebViewClient : WebViewClient
      {
#pragma warning disable 672
         public override bool ShouldOverrideUrlLoading(WebView webView, string url)
#pragma warning restore 672
         {
            // If the URL is not our own custom scheme, just let the webView load the URL as usual
            const string scheme = "hybrid:";

            if (!url.StartsWith(scheme))
               return false;

            // This handler will treat everything between the protocol and "?"
            // as the method name.  The querystring has all of the parameters.
            var resources = url.Substring(scheme.Length).Split('?');
            var method = resources[0];
            var parameters = HttpUtility.ParseQueryString(resources[1]);

            if (method == "UpdateLabel")
            {
               var textbox = parameters["textbox"];

               // Add some text to our string here so that we know something
               // happened on the native part of the round trip.
               var prepended = $"C# says \"{textbox}\"";

               // Build some javascript using the C#-modified result
               var js = $"SetLabelText('{prepended}');";

               webView.LoadUrl("javascript:" + js);
            }

            return true;
         }
      }
   }
}