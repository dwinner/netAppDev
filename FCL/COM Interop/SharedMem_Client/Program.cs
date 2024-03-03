namespace SharedMem_Client;

internal static class Program
{
   private static unsafe void Main()
   {
      using var sm = new SharedMem("MyShare", true, (uint)sizeof(MySharedData));
      var root = sm.Root.ToPointer();
      var data = (MySharedData*)root;

      Console.WriteLine($"Value is {data->Value}");
      Console.WriteLine($"Letter is {data->Letter}");
      Console.WriteLine($"11th Number is {data->Numbers[10]}");

      // Our turn to update values in shared memory!
      data->Value++;
      data->Letter = '!';
      data->Numbers[10] = 987.5f;
      Console.WriteLine("Updated shared memory");
      Console.ReadLine();
   }
}