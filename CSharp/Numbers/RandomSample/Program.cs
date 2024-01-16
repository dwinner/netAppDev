// If given the same seed, the random number series will be the same:

using System.Security.Cryptography;

var r1 = new Random(1);
var r2 = new Random(1);
Console.WriteLine(r1.Next(100) + ", " + r1.Next(100)); // 24, 11
Console.WriteLine(r2.Next(100) + ", " + r2.Next(100)); // 24, 11

// Using system clock for seed:
var r3 = new Random();
var r4 = new Random();
Console.WriteLine(r3.Next(100) + ", " + r3.Next(100)); // ?, ?
Console.WriteLine(r4.Next(100) + ", " + r4.Next(100)); // ", "
// Notice we still get same sequences, because of limitations in system clock resolution.

// Here's a workaround:
var r5 = new Random(Guid.NewGuid().GetHashCode());
var r6 = new Random(Guid.NewGuid().GetHashCode());
Console.WriteLine(r5.Next(100) + ", " + r5.Next(100)); // ?, ?
Console.WriteLine(r6.Next(100) + ", " + r6.Next(100)); // ?, ?

// From .NET 8, the Random class includes a GetItems method, which picks n random items from a collection.
int[] numbers = { 10, 20, 30, 40, 50 };

// Two random items
var randomTwo = new Random().GetItems(numbers, 2);
foreach (var rnd in randomTwo)
{
   Console.WriteLine(rnd);
}

// Shuffle (also new to .NET 8) shuffles items in an array:
new Random().Shuffle(numbers);
foreach (var number in numbers)
{
   Console.WriteLine(number);
}

// Random is not crytographically strong (the following, however, is):
var rand = RandomNumberGenerator.Create();
var bytes = new byte[4];
rand.GetBytes(bytes); // Fill the byte array with random numbers.

var strongRnd = BitConverter.ToInt32(bytes, 0);
Console.WriteLine($"A cryptographically strong random integer: {strongRnd}");