using System;

namespace EnumExtensions
{
   /// <summary>
   ///    Атрибут для культур
   /// </summary>
   [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
   internal class CultureAttribute : Attribute
   {
      public CultureAttribute(string culture) => Culture = culture;

      public string Culture { get; }
   }
}