var client = new HttpClient();

// The GetAsync method also accepts a CancellationToken.
var response = await client.GetAsync("http://www.linqpad.net")
   .ConfigureAwait(false);

response.EnsureSuccessStatusCode();
var html = await response.Content.ReadAsStringAsync()
   .ConfigureAwait(false);

Console.WriteLine(html);