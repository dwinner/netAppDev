using System.ComponentModel;
using System.Reflection;

Action<int> a =
   [Description("Method")] [return: Description("Return value")]
   ([Description("Parameter")] x) => Console.WriteLine(x);

var methodAttr = a.GetMethodInfo().GetCustomAttributes().Cast<DescriptionAttribute>().FirstOrDefault()?.Description
                 ?? string.Empty;
Console.WriteLine(methodAttr);

var paramAttr = a.GetMethodInfo().GetParameters()[0].GetCustomAttributes().Cast<DescriptionAttribute>().FirstOrDefault()
                   ?.Description
                ?? string.Empty;
Console.WriteLine(paramAttr);

var returnAttr = a.GetMethodInfo().ReturnParameter.GetCustomAttributes().Cast<DescriptionAttribute>().FirstOrDefault()
                    ?.Description
                 ?? string.Empty;
Console.WriteLine(returnAttr);