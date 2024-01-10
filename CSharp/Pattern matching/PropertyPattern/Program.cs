Console.WriteLine(ShouldAllow(new Uri("http://www.linqpad.net")));
Console.WriteLine(ShouldAllow(new Uri("ftp://ftp.microsoft.com")));
Console.WriteLine(ShouldAllow(new Uri("tcp:foo.database.windows.net")));

return;

bool ShouldAllow(Uri uri) => uri switch
{
   { Scheme: "http", Port: 80, Host: var host } => host.Length < 1000,
   { Scheme: "https", Port: 443 }
      when uri.Host.Length < 1000 => true,
   { Scheme: "ftp", Port: 21 } => true,
   { IsLoopback: true } => true,
   _ => false
};