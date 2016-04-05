// Определение типа, реализующего интерфейс ISerializable,
// не реализуемый базовым классом

using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace NotSerializedBaseTypeIssue
{
   internal static class Program
   {
      private static void Main()
      {
      }
   }

   [Serializable]
   internal class Base
   {
      private string _name = "Denny";

      public override string ToString()
      {
         return $"Name: {_name}";
      }
   }

   [Serializable]
   internal sealed class Derived : Base, ISerializable
   {
      private const string DateSerId = "Date";
      private readonly DateTime _date = DateTime.Now;

      public Derived()
      {
      }

      [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
      private Derived(SerializationInfo info, StreamingContext context)
      {
         // Получение набора сериализуемых членов для нашего и базовых классов
         var baseType = GetType().BaseType;
         Debug.Assert(baseType != null);
         var serializableMembers = FormatterServices.GetSerializableMembers(baseType, context);

         // Десериализация полей базового класса из объекта данных
         foreach (var fieldInfo in serializableMembers.Select(memberInfo => memberInfo as FieldInfo))
         {
            // Получение поля и присвоение ему десериализованного значения
            fieldInfo?.SetValue(this, info.GetValue($"{baseType.FullName}+{fieldInfo.Name}", fieldInfo.FieldType));
         }

         // Десериализация значений, сериализованных для этого класса
         _date = info.GetDateTime(DateSerId);
      }

      [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
      public void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         // Сериализация нужных значений для этого класса
         info.AddValue(DateSerId, _date);

         // Сериализация нужных значений для нашего и базовых классов
         var baseType = GetType().BaseType;
         Debug.Assert(baseType != null);
         var serializableMembers = FormatterServices.GetSerializableMembers(baseType, context);

         // Сериализация полей базового класса в объект данных
         foreach (var memInfo in serializableMembers)
         {
            // Полное имя базового типа ставим в префикс имени поля
            info.AddValue($"{baseType.FullName}+{memInfo.Name}", ((FieldInfo)memInfo).GetValue(this));
         }
      }

      public override string ToString()
      {
         return $"{base.ToString()}, Date: {_date}";
      }
   }
}