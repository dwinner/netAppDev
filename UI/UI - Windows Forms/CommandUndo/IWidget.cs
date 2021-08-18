using System.Drawing;

namespace CommandUndo
{
   /// <summary>
   /// Интерфейс бизнес-объектов
   /// </summary>
   public interface IWidget
   {
      void Draw(Graphics graphics);

      bool HitTest(Point point);

      Point Location { get; set; }

      Size Size { get; set; }

      Rectangle BoundingBox { get; }
   }
}
