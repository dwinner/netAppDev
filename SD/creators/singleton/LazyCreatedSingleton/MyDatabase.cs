namespace LazyCreatedSingleton;

public class MyDatabase
{
   private static readonly Lazy<MyDatabase> Impl = new(() => new MyDatabase());

   private MyDatabase()
   {
      Console.WriteLine("Initializing database");
   }

   public static MyDatabase Instance => Impl.Value;
}