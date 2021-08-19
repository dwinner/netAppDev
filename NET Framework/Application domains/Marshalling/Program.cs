// Доступ к объектам из других доменов

using System;
using System.Reflection;
using System.Threading;
using static System.Console;
using static System.Runtime.Remoting.RemotingServices;

namespace Marshalling
{
   internal static class Program
   {
      private static void Main()
      {
         var domain = Thread.GetDomain();
         var callingDomainName = domain.FriendlyName;
         WriteLine("Default AppDomain's friendly name={0}", callingDomainName);
         var exeAssembly = Assembly.GetEntryAssembly().FullName;
         WriteLine("Main assembly={0}", exeAssembly);

         #region Доступ к объектам другого домена приложений с продвижением по ссылке

         WriteLine("{0}Demo #1", Environment.NewLine);

         var newAppDomain = AppDomain.CreateDomain("AD #2", null, null);
         // Получаем ссылку на proxy
         var marshalObj =
            (MarshalByRefType) newAppDomain.CreateInstanceAndUnwrap(exeAssembly, nameof(MarshalByRefType));
         WriteLine("Type={0}", marshalObj.GetType()); // CRL неверно определит тип
         WriteLine("Is proxy={0}", IsTransparentProxy(marshalObj)); // Действительно: это ссылка на proxy
         marshalObj.SomeMethod();
         AppDomain.Unload(newAppDomain);

         // Домен уже выгружен, поэтому получим исключение
         try
         {
            marshalObj.SomeMethod();
            WriteLine("Successfull call");
         }
         catch (AppDomainUnloadedException)
         {
            WriteLine("Failed call");
         }

         #endregion

         #region Доступ к объектам другого домена с продвижением по значению

         WriteLine("{0}Demo #2", Environment.NewLine);
         newAppDomain = AppDomain.CreateDomain("AD #2", null, null);
         marshalObj = (MarshalByRefType) newAppDomain.CreateInstanceAndUnwrap(exeAssembly, nameof(MarshalByRefType));
         var byValType = marshalObj.MethodWithReturn(); // Метод возвращает копию объекта
         WriteLine("Is proxy={0}", IsTransparentProxy(byValType)); // Это уже не proxy
         WriteLine("Returned object created {0}", byValType);
         AppDomain.Unload(newAppDomain);

         // marshalObj ссылается на действительный объект: выгрузка домена не имеет никакого эффекта
         try
         {
            WriteLine("Returned object created {0}", byValType);
            WriteLine("Successful call");
         }
         catch (AppDomainUnloadedException)
         {
            WriteLine("Failed call");
         }

         #endregion

         #region Доступ к объектам другого домена без использования механизма продвижения

         WriteLine("{0}Demo #3", Environment.NewLine);
         newAppDomain = AppDomain.CreateDomain("AD #2", null, null);
         marshalObj = (MarshalByRefType) newAppDomain.CreateInstanceAndUnwrap(exeAssembly, nameof(MarshalByRefType));
         var failedCall = marshalObj.MethodArgAndReturn(callingDomainName); // Сгенерируется исключение
         WriteLine(failedCall);

         #endregion
      }
   }
}