using System;

namespace ObjectsCloning
{
   public sealed class Dimensions : ICloneable   // Note: Клонирование копирующим конструктором
   {
      private readonly long _width;
      private readonly long _height;

      private const long DefaultWidth = 0;
      private const long DefaultHeight = 0;

      public Dimensions()
      {
         _width = DefaultWidth;
         _height = DefaultHeight;
      }

      public Dimensions(long width, long height)
      {
         _width = width;
         _height = height;
      }

      // Приватный копирующий конструктор,
      // используемый при создании копии этого объекта
      private Dimensions(Dimensions otherDimensions)
      {
         _width = otherDimensions._width;
         _height = otherDimensions._height;
      }

      #region Реализация IClonable

      public object Clone()
      {
         return new Dimensions(this);
      }

      #endregion

   }
}