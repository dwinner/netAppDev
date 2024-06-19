using JetBrains.dotMemoryUnit;
using static System.Console;

namespace UserNamesFlyweight;

public class UserTests
{
   [Test]
   public void TestUser1()
   {
      var users = new List<User>();

      var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
      var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

      foreach (var firstName in firstNames)
      foreach (var lastName in lastNames)
      {
         users.Add(new User($"{firstName} {lastName}"));
      }

      ForceGc();

      dotMemory.Check(memory => { WriteLine(memory.SizeInBytes); });
   }

   [Test]
   public void TestUser2()
   {
      var users = new List<User2>();

      var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
      var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

      foreach (var firstName in firstNames)
      foreach (var lastName in lastNames)
      {
         users.Add(new User2($"{firstName} {lastName}"));
      }

      ForceGc();

      dotMemory.Check(memory => { WriteLine(memory.SizeInBytes); });
   }

   public void ForceGc()
   {
      GC.Collect();
      GC.WaitForPendingFinalizers();
      GC.Collect();
   }

   public static string RandomString()
   {
      var rand = new Random();
      return new string(
         Enumerable.Range(0, 10).Select(i => (char)('a' + rand.Next(26))).ToArray());
   }
}