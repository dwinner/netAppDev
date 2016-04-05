/**
 * API-интерфейс развертывания приложений для Windows 8
 */

using System;
using System.Collections.Generic;
using System.IO;
using Windows.ApplicationModel;
using Windows.Management.Deployment;

namespace Win8PackageSample
{
   class Program
   {
      static void Main()
      {
         var packageManager = new PackageManager();
         IEnumerable<Package> packages = packageManager.FindPackages();
         foreach (var package in packages)
         {
            try
            {
               Console.WriteLine("Architecture: {0}", package.Id.Architecture);
               Console.WriteLine("Family: {0}", package.Id.FamilyName);
               Console.WriteLine("Full name: {0}", package.Id.FullName);
               Console.WriteLine("Name: {0}", package.Id.Name);
               Console.WriteLine("Publisher: {0}", package.Id.Publisher);
               Console.WriteLine("Publisher Id: {0}", package.Id.PublisherId);
               if (package.InstalledLocation != null)
               {
                  Console.WriteLine(package.InstalledLocation.Path);
               }
               Console.WriteLine();
            }
            catch (FileNotFoundException ex)
            {
               Console.WriteLine("{0}, file: {1}", ex.Message, ex.FileName);
            }
         }

         Console.ReadLine();
      }
   }
}
