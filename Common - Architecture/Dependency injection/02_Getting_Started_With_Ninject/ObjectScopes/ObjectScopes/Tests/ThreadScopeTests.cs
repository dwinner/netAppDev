using System.Threading;
using Ninject;
using NUnit.Framework;

namespace ObjectScopes.Tests
{
   [TestFixture]
   internal class ThreadScopeTests
   {
      [Test]
      public void ReturnsDifferentInstancesInDifferentThreads()
      {
         var kernel = new StandardKernel();
         kernel.Bind<object>().ToSelf().InThreadScope();

         var instance1 = kernel.Get<object>();

         new Thread(() =>
         {
            var instance2 = kernel.Get<object>();
            Assert.AreNotEqual(instance1, instance2);
            kernel.Dispose();
         }).Start();
      }

      [Test]
      public void ReturnsTheSameInstancesInOneThread()
      {
         using (var kernel = new StandardKernel())
         {
            kernel.Bind<object>().ToSelf().InThreadScope();

            var instance1 = kernel.Get<object>();
            var instance2 = kernel.Get<object>();

            Assert.AreEqual(instance1, instance2);
         }
      }
   }
}