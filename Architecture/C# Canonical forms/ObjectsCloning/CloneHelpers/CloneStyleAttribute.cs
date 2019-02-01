using System;

namespace ObjectsCloning.CloneHelpers
{
   /// <summary>
   /// Аттрибут для маркировки способа клонирования объекта
   /// </summary>
   [AttributeUsage(AttributeTargets.Method)]
   public class CloneStyleAttribute : Attribute
   {
      private readonly CloneStyle _cloneStyle;

      public CloneStyle CloneStyle { get { return _cloneStyle; } }

      public CloneStyleAttribute(CloneStyle cloneStyle)
      {
         _cloneStyle = cloneStyle;
      }      
   }
}