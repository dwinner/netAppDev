using MoreLinq;

int[] numbers = { 3, 5, 2, 234, 4, 1 };

numbers.TakeWhile(n => n < 100).ForEach(Console.WriteLine);
numbers.SkipWhile(n => n < 100).ForEach(Console.WriteLine);