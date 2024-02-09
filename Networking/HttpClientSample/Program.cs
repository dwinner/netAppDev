var client = new HttpClient();
var task1 = client.GetStringAsync("http://www.linqpad.net");
var task2 = client.GetStringAsync("http://www.albahari.com");

var len1 = (await task1).Length;
var len2 = (await task2).Length;

Console.WriteLine(len1);
Console.WriteLine(len2);