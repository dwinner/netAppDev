using System;

namespace AttributeDemo
{
   [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
   class CultureAttribute : Attribute
   {
      private readonly string _culture;

      public string Culture { get { return _culture; } }

      public CultureAttribute(string culture)
      {
         _culture = culture;
      }
   }
}
