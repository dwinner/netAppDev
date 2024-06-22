namespace PropertyObserver;

internal static class Program
{
   private static void Main()
   {
      var p = new Person();
      p.PropertyChanged += (sender, eventArgs) => { Console.WriteLine($"{eventArgs.PropertyName} changed"); };
      p.Age = 15;
      p.Age++;
   }
}