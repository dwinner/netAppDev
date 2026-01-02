using static System.Console;

var temp = 5;
Func<int, int> doubleMaker = x => x * 2;
var result = Container.GetResult(doubleMaker, temp);
WriteLine(result);

internal static class Container
{
   public static int GetResult(Func<int, int> f, int x) => f(x);
}