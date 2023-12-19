using System;
using NUnit.Framework;

namespace ComplexNumberNUnit
{
   [SetUpFixture]
   public class MySetUpClass
   {
      [OneTimeSetUp]
      public void RunBeforeAnyTests()
      {
         Console.WriteLine(nameof(RunBeforeAnyTests));
      }

      [OneTimeTearDown]
      public void RunAfterAnyTests()
      {
         Console.WriteLine(nameof(RunAfterAnyTests));
      }
   }
}