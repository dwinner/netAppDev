// Применение аспектов для автоматической привязки других атрибутов

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using PostSharp.Reflection;

namespace AutoDataContractMaker
{
   internal static class Program
   {
      private static void Main()
      {
      }
   }

   // Мы устанавливаем наследование так, чтобы аспект был автоматически добавлен к дочерним типам
   [MulticastAttributeUsage(MulticastTargets.Class, Inheritance = MulticastInheritance.Strict)]
   [Serializable]
   public sealed class AutoDataContractAttribute : TypeLevelAspect, IAspectProvider
   {
      // Этот метод вызывается во время сборки и нужен другим аспектам
      public IEnumerable<AspectInstance> ProvideAspects(object targetElement)
      {
         var targetType = (Type)targetElement;
         var datacontractIntroductionAspect =
            new CustomAttributeIntroductionAspect(
               new ObjectConstruction(typeof(DataContractAttribute).GetConstructor(Type.EmptyTypes)));
         var datamemberIntroductionAspect =
            new CustomAttributeIntroductionAspect(
               new ObjectConstruction(typeof(DataMemberAttribute).GetConstructor(Type.EmptyTypes)));

         // Добавим атрибут DataContract к типу
         yield return new AspectInstance(targetElement, datacontractIntroductionAspect);

         // Добавим атрибут DataMember к каждому соответствующему свойству
         foreach (
            var property in
               targetType.GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance)
                  .Where(property => property.CanWrite && !property.IsDefined(typeof(NotDataMemberAttribute), false)))
         {
            yield return new AspectInstance(property, datamemberIntroductionAspect);
         }
      }
   }

   [AttributeUsage(AttributeTargets.Property)]
   public sealed class NotDataMemberAttribute : Attribute
   {
   }

   [AutoDataContract]
   public sealed class Person
   {
      public string FirstName { get; set; }
      public string LastName { get; set; }
   }
}