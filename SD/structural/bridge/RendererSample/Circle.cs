namespace RendererSample;

public class Circle(IRenderer renderer, float radius) : Shape(renderer)
{
   public override void Draw()
   {
      Renderer.RenderCircle(radius);
   }

   public override void Resize(float factor)
   {
      radius *= factor;
   }
}