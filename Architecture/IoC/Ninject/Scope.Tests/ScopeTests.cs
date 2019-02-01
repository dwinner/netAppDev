// Время жизни объектов при внедрении

using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using NUnit.Framework;

namespace Scope.Tests
{
   [TestFixture]
   public class ScopeTests
   {
      [Test]
      public void CustomScope()
      {
         var kernel = new StandardKernel();
         kernel.Bind<ScopeObject>().ToSelf().InScope(context => ProcessingScope.Current);

         var scopeA = new ScopeObject();
         var scopeB = new ScopeObject();

         ProcessingScope.Current = scopeA;
         var testA1 = kernel.Get<ScopeObject>();
         var testA2 = kernel.Get<ScopeObject>();

         Assert.AreSame(testA1, testA2);

         ProcessingScope.Current = scopeB;
         var testB = kernel.Get<ScopeObject>();

         Assert.AreNotSame(testB, testA1);

         ProcessingScope.Current = scopeA;
         var testA3 = kernel.Get<ScopeObject>();

         Assert.AreSame(testA3, testA1);
      }

      [Test]
      public void SingleScope()
      {
         IKernel kernel = new StandardKernel();
         kernel.Bind<ScopeObject>().ToSelf().InSingletonScope();

         var tasks = new Task[10];
         var objects = new ConcurrentBag<ScopeObject>();
         for (var i = 0; i < tasks.Length; i++)
         {
            tasks[i] = new Task(() =>
            {
               var currentTaskObj = kernel.Get<ScopeObject>();
               objects.Add(currentTaskObj);
            });
            tasks[i].Start();
         }

         Task.WaitAll(tasks);

         var scopeObjects = objects.ToArray();
         for (var i = 0; i < scopeObjects.Length; i++)
         {
            for (var j = i; j < scopeObjects.Length; j++)
            {
               Assert.AreSame(scopeObjects[i], scopeObjects[j]);
            }
         }
      }

      [Test]
      public void ThreadScope()
      {
         IKernel kernel = new StandardKernel();
         kernel.Bind<ScopeObject>().ToSelf().InThreadScope();

         Task.Factory.StartNew(() =>
         {
            var o1 = kernel.Get<ScopeObject>();
            var newThread = new Thread(o =>
            {
               var o2 = kernel.Get<ScopeObject>();
               Assert.AreNotSame(o1, o2);
            });

            newThread.Start();
            newThread.Join();
         }).Wait();
      }
   }

   public class ScopeObject
   {
   }

   public static class ProcessingScope
   {
      public static ScopeObject Current { get; set; }
   }
}