int[] numbers = { 3, 5, 7 };
string[] words = { "three", "five", "seven", "ignored" };
var zip = numbers.Zip(words, (n, w) => n + "=" + w);

foreach (var str in zip)
{
   Console.WriteLine(str);
}