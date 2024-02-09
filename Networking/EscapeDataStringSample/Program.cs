using System.Diagnostics;

var search = Uri.EscapeDataString("(WebClient OR HttpClient)");
var language = Uri.EscapeDataString("fr");
var requestUri = "http://www.google.com/search?q=" + search +
                 "&hl=" + language;

var html = await new HttpClient().GetStringAsync(requestUri)
   .ConfigureAwait(false);
File.WriteAllText("temp.html", html);
Process.Start(new ProcessStartInfo("temp.html") { UseShellExecute = true });