namespace LazyCreatedSingleton;

public class Database
{
   private Database()
   {
   }

   public static Database Instance { get; } = new();
}