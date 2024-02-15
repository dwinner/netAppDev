#nullable disable

namespace ThreadSafety_AppServer;

internal static class Program
{
   private static void Main()
   {
      new Thread(() => Console.WriteLine(UserCache.GetUser(1))).Start();
      new Thread(() => Console.WriteLine(UserCache.GetUser(1))).Start();
      new Thread(() => Console.WriteLine(UserCache.GetUser(1))).Start();
   }
}

internal static class UserCache
{
   private static readonly Dictionary<int, User> Users = new();

   internal static User GetUser(int id)
   {
      User user;
      lock (Users)
      {
         if (Users.TryGetValue(id, out user))
         {
            return user;
         }
      }

      user = RetrieveUser(id); // Method to retrieve from database;
      lock (Users)
      {
         Users[id] = user;
      }

      return user;
   }

   private static User RetrieveUser(int id)
   {
      Thread.Sleep(1000); // simulate a time-consuming operation
      return new User { Id = id };
   }
}

internal class User
{
   public int Id { get; set; }

   public override string ToString() => $"{nameof(Id)}: {Id}";
}