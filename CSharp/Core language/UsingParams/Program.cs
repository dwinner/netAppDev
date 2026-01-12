List<int> nums = [1, 2, 3, 4, 5];
Console.WriteLine(Sum(nums));
Console.WriteLine(Sum(1, 2, 3, 4));
Console.WriteLine(Sum(1, 2, 3));

return;

int Sum(params List<int> numbers)
{
   return numbers.Sum();
}