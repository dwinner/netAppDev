namespace Graphic.Core
{
   public class ShapeService
   {
      private readonly IShapeFactory _factory;

      public ShapeService(IShapeFactory factory) => _factory = factory;

      public void AddShapes(int circles, int squares, ICanvas canvas)
      {
         for (var i = 0; i < circles; i++)
         {
            var circle = _factory.CreateCircle();
            canvas.AddShape(circle);
         }

         for (var i = 0; i < squares; i++)
         {
            var square = _factory.CreateSquare();
            canvas.AddShape(square);
         }
      }
   }
}