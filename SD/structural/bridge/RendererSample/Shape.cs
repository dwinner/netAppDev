namespace RendererSample;

// a bridge between the shape that's being drawn
// the component which actually draws it

public abstract class Shape(IRenderer renderer)
{
   protected readonly IRenderer Renderer = renderer;

   public abstract void Draw();

   public abstract void Resize(float factor);
}