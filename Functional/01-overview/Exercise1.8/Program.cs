using static System.Console;

WriteLine("Exercise 1.8");
Sample sample = new();
WriteLine(Sample.OpsDone); // False
Sample.ChangeStatus();
WriteLine(Sample.OpsDone); // True
Sample.ChangeStatus();
WriteLine(Sample.OpsDone); // False


WriteLine(Sample.FindDifference(23, 19));
WriteLine(Sample.FindDifference(19, 23));

WriteLine(sample.GetCurrentTime());
Thread.Sleep(1000);
WriteLine(Sample.Get75());
WriteLine(Sample.GetSquare(3));

WriteLine(Sample.GetCube(3));
WriteLine(Sample.OpsDone);

WriteLine(sample.GetCurrentTime());
sample.DoSomething("some input");

PositiveNumber positive1 = new(12);
PositiveNumber positive2 = new(0);

WriteLine(sample.DividePositiveNumbers(positive1, positive2));

internal class Sample
{
   public static bool OpsDone { get; set; }

   public static void ChangeStatus()
   {
      OpsDone = !OpsDone;
   }

   public static int FindDifference(int a, int b) => Math.Abs(a - b);

   public static int GetSquare(int x) => x * x;

   public static int GetCube(int x)
   {
      OpsDone = true;
      return x * x * x;
   }

   public int Divide(int a, int b) => a / b;

   internal double DividePositiveNumbers(PositiveNumber a, PositiveNumber b) => a.Number / b.Number;

   public void DoSomething(string input)
   {
      WriteLine($"Doing some specific operation based on {input}.");
      OpsDone = true;
   }

   public static int Get75() => 75;

   public DateTime GetCurrentTime() => DateTime.Now;
}

internal class PositiveNumber
{
   public PositiveNumber(int input) => Number = input > 0 ? input : 1;

   public double Number { get; }
}