const string uri = "http://www.albahari.com/EchoPost.aspx";
var client = new HttpClient();
var dict = new Dictionary<string, string>
{
   { "Name", "Joe Albahari" },
   { "Company", "O'Reilly" }
};
var values = new FormUrlEncodedContent(dict);
var response = await client.PostAsync(uri, values)
   .ConfigureAwait(false);
response.EnsureSuccessStatusCode();

var postResult = await response.Content.ReadAsStringAsync()
   .ConfigureAwait(false);

Console.WriteLine(Uri.UnescapeDataString(postResult));