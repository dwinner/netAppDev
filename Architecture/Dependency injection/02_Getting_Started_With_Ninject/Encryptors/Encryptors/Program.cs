using System;
using Ninject;

namespace Samples.Encryption
{
   internal static class Program
   {
      private static void Main()
      {
         using (var kernel = new StandardKernel())
         {
            kernel.Load("typeRegistrations.xml");
            var encryptor = kernel.Get<IEncryptor>();
            Console.WriteLine(encryptor.Encrypt("Hello"));
            Console.ReadKey();
         }
      }
   }
}