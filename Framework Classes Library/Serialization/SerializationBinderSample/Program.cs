// Переопределение сборки и/или типа при десериализации объекта

using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace SerializationBinderSample
{
   internal static class Program
   {
      private static void Main()
      {
      }
   }

   internal sealed class Ver1ToVer2SerializationBinder : SerializationBinder
   {
      public override Type BindToType(string assemblyName, string typeName)
      {
         // Десериализация объекта Ver1 из версии 1.0.0.0 в объект Ver2

         // Вычисление имени сборки, определяющей тип Ver1
         var assemblyVer1 = Assembly.GetExecutingAssembly().GetName();
         assemblyVer1.Version = new Version(1, 0, 0, 0);
         return assemblyName == assemblyVer1.ToString() && typeName == nameof(Ver1)
            ? typeof (Ver2) // При десериализации объекта Ver1 версии v1.0.0.0 превращаем его в Ver2
            : Type.GetType($"{typeName}, {assemblyName}"); // В противном случае возвращаем запрошенный тип
      }
   }

   internal class Ver1
   {
   }

   internal class Ver2
   {
   }
}