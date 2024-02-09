using System.Net;
using System.Text;

namespace SimpleHttpServer;

internal class WebServer
{
   private readonly string _baseFolder; // Your web page folder.
   private readonly HttpListener _listener;

   public WebServer(string uriPrefix, string baseFolder)
   {
      _listener = new HttpListener();
      _listener.Prefixes.Add(uriPrefix);
      _baseFolder = baseFolder;
   }

   public async void Start()
   {
      _listener.Start();
      while (true)
      {
         try
         {
            var context = await _listener.GetContextAsync()
               .ConfigureAwait(false);
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(() => ProcessRequestAsync(context));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
         }
         catch (HttpListenerException)
         {
            break;
         } // Listener stopped.
         catch (InvalidOperationException)
         {
            break;
         } // Listener stopped.
      }
   }

   public void Stop()
   {
      _listener.Stop();
   }

   private async void ProcessRequestAsync(HttpListenerContext context)
   {
      try
      {
         var filename = Path.GetFileName(context.Request.RawUrl);
         var path = Path.Combine(_baseFolder, filename!);
         byte[] msg;
         if (!File.Exists(path))
         {
            Console.WriteLine("Resource not found: {0}", path);
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            msg = Encoding.UTF8.GetBytes("Sorry, that page does not exist");
         }
         else
         {
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            msg = await File.ReadAllBytesAsync(path)
               .ConfigureAwait(false);
         }

         context.Response.ContentLength64 = msg.Length;
         await using var oStream = context.Response.OutputStream;
         await oStream.WriteAsync(msg, 0, msg.Length)
            .ConfigureAwait(false);
      }
      catch (Exception ex)
      {
         Console.WriteLine($"Request error: {ex}");
      }
   }
}