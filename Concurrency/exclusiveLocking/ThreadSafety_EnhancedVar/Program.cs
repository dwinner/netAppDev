namespace ThreadSafety_EnhancedVar;

internal static class Program
{
   private static async Task Main()
   {
      new Thread(() => Console.WriteLine(UserCache.GetUserAsync(1))).Start();
      new Thread(() => Console.WriteLine(UserCache.GetUserAsync(1))).Start();
      new Thread(() => Console.WriteLine(UserCache.GetUserAsync(1))).Start();

      // You can also await this method:
      var user = await UserCache.GetUserAsync(1).ConfigureAwait(false);
      Console.WriteLine(user);
   }
}

internal static class UserCache
{
   private static readonly Dictionary<int, Task<User>> UserTasks = new();

   internal static Task<User> GetUserAsync(int id)
   {
      lock (UserTasks)
      {
         return UserTasks.TryGetValue(id, out var userTask)
            ? userTask
            : UserTasks[id] = Task.Run(() => RetrieveUser(id));
      }
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