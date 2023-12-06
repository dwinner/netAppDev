namespace FourAspects;

public class HeaderRenderer : IRenderer
{
   public string Render(Message message) => $"<h1>{message.Header}</h1>";
}