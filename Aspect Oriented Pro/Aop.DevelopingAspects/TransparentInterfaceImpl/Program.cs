// Прозрачное внедрение интерфейса

using System;
using System.Collections.Generic;
using PostSharp;
using PostSharp.Aspects;

namespace TransparentInterfaceImpl
{
   internal static class Program
   {
      private static void Main()
      {
         var compose = new TestCompose();
         compose.Test();
      }
   }

   [Serializable]
   [LinesOfCodeAvoided(default(int))]
   public sealed class GeneralComposeAttribute : CompositionAspect
   {
      private readonly Type _implementationType;
      [NonSerialized]
      private readonly Type _interfaceType;

      public GeneralComposeAttribute(Type interfaceType, Type implementationType)
      {
         _interfaceType = interfaceType;
         _implementationType = implementationType;
      }

      // Вызывется в момент сборки. Мы возвращаем интерфейс, который хотим реализовать
      protected override Type[] GetPublicInterfaces(Type targetType) => new[] { _interfaceType };

      // Вызывается в момент выполнения
      public override object CreateImplementationObject(AdviceArgs args)
         => Activator.CreateInstance(_implementationType);
   }

   [GeneralCompose(typeof(IList<string>), typeof(List<string>))]
   public class TestCompose
   {
      public void Test()
      {
         var list = Post.Cast<TestCompose, IList<string>>(this);
         list.Add("apple");
         list.Add("orange");
         list.Add("banana");

         foreach (var item in list)
         {
            Console.WriteLine(item);
         }
      }
   }
}