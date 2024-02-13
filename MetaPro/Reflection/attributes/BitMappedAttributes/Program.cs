using System.Reflection;

var ta = typeof(Console).Attributes;
Console.WriteLine(ta);

var ma = MethodBase.GetCurrentMethod().Attributes;
Console.WriteLine(ma);