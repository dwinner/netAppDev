using static System.Console;

WriteLine("Exercise 5.1");
Func<int, Func<int, Func<int, int>>> sum = x => y => z => x + y + z;
var temp1 = sum(2);
var temp2 = temp1(5);
var result = temp2(7);
WriteLine($"2+5+7={result}");