using Ninject;
using NUnit.Framework;

namespace ObjectScopes.Tests
{
   [TestFixture]
   internal class CustomScopeTests
   {
      [Test]
      public void ReturnsDifferentInstancesForDifferentUsers()
      {
         using (var kernel = new StandardKernel())
         {
            kernel.Bind<object>().ToSelf().InScope(ctx => User.Current);
            User.Current = new User();
            var instance1 = kernel.Get<object>();
            User.Current = new User();
            var instance2 = kernel.Get<object>();
            Assert.AreNotEqual(instance1, instance2);
         }
      }

      [Test]
      public void ReturnsTheSameInstancesForAUser()
      {
         using (var kernel = new StandardKernel())
         {
            kernel.Bind<object>().ToSelf().InScope(ctx => User.Current);
            User.Current = new User();
            var instance1 = kernel.Get<object>();
            User.Current.Name = "Foo";
            var instance2 = kernel.Get<object>();
            Assert.AreEqual(instance1, instance2);
         }
      }
   }

   internal class User
   {
      public string Name { get; set; }
      public static User Current { get; set; }
   }
}