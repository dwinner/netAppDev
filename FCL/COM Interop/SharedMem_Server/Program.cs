namespace SharedMem_Server;

internal static class Program
{
   private static unsafe void Main()
   {
      using var sm = new SharedMem("MyShare", false, (uint)sizeof(MySharedData));
      var root = sm.Root.ToPointer();
      var data = (MySharedData*)root;

      Console.Write("Before this process writes to shared mem:");
      Console.WriteLine($"Value is {data->Value}");
      Console.WriteLine($"Letter is {data->Letter}");
      Console.WriteLine($"11th Number is {data->Numbers[10]}");

      data->Value = 123;
      data->Letter = 'X';
      data->Numbers[10] = 1.45f;
      Console.WriteLine("Written to shared memory");

      Console.ReadLine();

      Console.WriteLine($"Value is {data->Value}");
      Console.WriteLine($"Letter is {data->Letter}");
      Console.WriteLine($"11th Number is {data->Numbers[10]}");
      Console.ReadLine();
   }
}