using System;
using System.Drawing;

namespace SimplePaintProgram
{
   [Serializable]
   public class ShapeData
   {
      /// <summary>
      ///    Верхняя левая координата фигуры
      /// </summary>
      public Point UpperLeftPoint { get; set; }

      /// <summary>
      ///    Текущий цвет фигуры
      /// </summary>
      public Color Color { get; set; }

      /// <summary>
      ///    Вид фигуры
      /// </summary>
      public SelectedShape SelectedShape { get; set; }
   }
}