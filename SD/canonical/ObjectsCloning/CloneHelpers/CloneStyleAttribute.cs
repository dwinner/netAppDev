using System;

namespace ObjectsCloning.CloneHelpers
{
   /// <summary>
   ///    Аттрибут для маркировки способа клонирования объекта
   /// </summary>
   [AttributeUsage(AttributeTargets.Method)]
   public class CloneStyleAttribute : Attribute
   {
      public CloneStyleAttribute(CloneStyle cloneStyle) => CloneStyle = cloneStyle;

      public CloneStyle CloneStyle { get; }
   }
}