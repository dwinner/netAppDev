using System.Net;

var cookieContainer = new CookieContainer();
var handler = new HttpClientHandler { CookieContainer = cookieContainer };
var client = new HttpClient(handler);
await client.GetStringAsync("http://www.google.com")
   .ConfigureAwait(false);
var cookies = cookieContainer.GetAllCookies();
for (var index = 0; index < cookies.Count; index++)
{
   var cookie = cookies[index];
   Console.WriteLine(cookie);
}