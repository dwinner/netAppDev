using System;

namespace EnumExtensions
{
   /// <summary>
   /// Атрибут для культур
   /// </summary>
   [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
   class CultureAttribute : Attribute
   {
      private readonly string _culture;

      public CultureAttribute(string culture)
      {
         _culture = culture;
      }

      public string Culture
      {
         get { return _culture; }
      }
   }
}
