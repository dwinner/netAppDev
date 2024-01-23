using System.Linq.Expressions;

var p = Expression.Parameter(typeof(string), "s");
var stringLength = Expression.Property(p, "Length");
var five = Expression.Constant(5);
var comparison = Expression.LessThan(stringLength, five);
var lambda = Expression.Lambda<Func<string, bool>>(comparison, p);
var runnable = lambda.Compile();

var kResult = runnable("kangaroo");
var dResult = runnable("dog");

Console.WriteLine(kResult);
Console.WriteLine(dResult);