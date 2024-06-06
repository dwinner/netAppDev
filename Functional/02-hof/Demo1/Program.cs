using static System.Console;

WriteLine("Evaluating the function: f(x)= 2x.");
List<int> domainSet = new() { 1, 2, 3, 4, 5 };
Write("Domain: [");
Helper.DisplayNumbers(domainSet);
Write("]");
WriteLine();

var rangeSet = Helper.MultiplyEachElementByTwo(domainSet);
Write("Range: [");
Helper.DisplayNumbers(rangeSet);
WriteLine("]");

internal static class Helper
{
   public static void DisplayNumbers(List<int> numberList)
   {
      foreach (var i in numberList)
      {
         Write(i + "\t");
      }
   }

   public static List<int> MultiplyEachElementByTwo(List<int> numberList)
   {
      List<int> temp = new();
      foreach (var i in numberList)
      {
         temp.Add(i * 2);
      }

      return temp;
   }
}