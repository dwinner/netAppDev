using System.Text;

var encoding = Encoding.UTF8;

// Call Encoding.GetEncoding with a standard IANA name to obtain an encoding:
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var chinese = Encoding.GetEncoding("GB18030");

// The static GetEncodings method returns a list of all supported encodings:
foreach (var info in Encoding.GetEncodings())
{
   Console.WriteLine(info.Name);
}