using TypeExtensions;

Console.WriteLine("hi there".GetCharCount('e'));
Console.WriteLine("777".IsNumber);
Console.WriteLine(string.Heaven);

int[] nums1 = [1, 2, 3, 4];
int[] nums2 = [6, 7, 8];
var nums3 = nums1 + nums2;
Console.WriteLine(nums3);

var text = "From C#14";

text >>= Console.WriteLine;
text >>= FramePrint;
text >>= msg => Console.WriteLine($"<{msg}>");

Person person = new("Bob", 44);
person >>= Console.WriteLine; // Bob (44)

person >>= p => p.Age = 25;
person >>= Console.WriteLine; // Bob (25)

person >>= SetDefault;
person >>= Console.WriteLine; // Tom (41)

var checkNum = (string? val) => val?.Length > 0 ? val : "11";
Console.Write("Enter a number: ");
var num =
      Console.ReadLine()
      | checkNum
      | int.Parse
      | Square
   ;
Console.WriteLine(num);

return;

int Square(int n) => n * n;

void FramePrint(string message) => Console.WriteLine($"***** {message} *****");

void SetDefault(Person aPerson)
{
   aPerson.Age = 41;
   aPerson.Name = "Tom";
}