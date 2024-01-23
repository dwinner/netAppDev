int[] numbers = { 1, 2, 3 };
var totalNum = numbers.Aggregate(0, (seed, n) => seed + n);
Console.WriteLine(totalNum);