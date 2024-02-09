var info = new Uri("http://www.domain.com:80/info/");
var page = new Uri("http://www.domain.com/info/page.html");

Console.WriteLine(info.Host); // www.domain.com
Console.WriteLine(info.Port); // 80
Console.WriteLine(page.Port); // 80  (Uri knows the default HTTP port)

Console.WriteLine(info.IsBaseOf(page)); // True
var relative = info.MakeRelativeUri(page);
Console.WriteLine(relative.IsAbsoluteUri); // False
Console.WriteLine(relative.ToString()); // page.html