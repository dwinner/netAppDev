namespace FourAspects;

public class FooterRenderer : IRenderer
{
   public string Render(Message message) => $"<i>{message.Footer}</i>";
}