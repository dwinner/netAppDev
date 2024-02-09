var client = new HttpClient(new HttpClientHandler { UseProxy = false });
var request = new HttpRequestMessage(
   HttpMethod.Post, "http://www.albahari.com/EchoPost.aspx");
request.Content = new StringContent("This is a test");
var response = await client.SendAsync(request)
   .ConfigureAwait(false);
response.EnsureSuccessStatusCode();
var postResult = await response.Content.ReadAsStringAsync()
   .ConfigureAwait(false);
Console.WriteLine(postResult);