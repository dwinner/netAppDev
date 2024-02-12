var unboundMethod = (
   from m in typeof(Enumerable).GetMethods()
   where m.Name == "Where" && m.IsGenericMethod
   let parameters = m.GetParameters()
   where parameters.Length == 2
   let genArg = m.GetGenericArguments().First()
   let enumerableOfT = typeof(IEnumerable<>).MakeGenericType(genArg)
   let funcOfTBool = typeof(Func<,>).MakeGenericType(genArg, typeof(bool))
   where parameters[0].ParameterType == enumerableOfT
         && parameters[1].ParameterType == funcOfTBool
   select m).Single();

Console.WriteLine(unboundMethod);

var closedMethod = unboundMethod.MakeGenericMethod(typeof(int));
Console.WriteLine(closedMethod);

int[] source = { 3, 4, 5, 6, 7, 8 };
Func<int, bool> predicate = n => n % 2 == 1; // Odd numbers only

// We can now invoke the closed generic method as follows:
var query = (IEnumerable<int>)closedMethod.Invoke
   (null, new object[] { source, predicate });

foreach (var i in query)
{
   Console.WriteLine(i);
}