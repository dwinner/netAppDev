﻿namespace FourAspects;

public class MessageRendererTests
{
   [Fact]
   public void Rendering_a_message()
   {
      var sut = new MessageRenderer();
      var message = new Message
      {
         Header = "h",
         Body = "b",
         Footer = "f"
      };

      var html = sut.Render(message);

      Assert.Equal("<h1>h</h1><b>b</b><i>f</i>", html);
   }

   [Fact]
   public void MessageRenderer_uses_correct_sub_renderers()
   {
      var sut = new MessageRenderer();

      var renderers = sut.SubRenderers;

      Assert.Equal(3, renderers.Count);
      Assert.IsAssignableFrom<HeaderRenderer>(renderers[0]);
      Assert.IsAssignableFrom<BodyRenderer>(renderers[1]);
      Assert.IsAssignableFrom<FooterRenderer>(renderers[2]);
   }

   [Fact(Skip = "Example of how not to write tests")]
   public void MessageRenderer_is_implemented_correctly()
   {
      var sourceCode = File.ReadAllText(@"<project path>\MessageRenderer.cs");

      Assert.Equal(
         @"
public class MessageRenderer : IRenderer
{
    public IReadOnlyList<IRenderer> SubRenderers { get; }

    public MessageRenderer()
    {
        SubRenderers = new List<IRenderer>
        {
            new HeaderRenderer(),
            new BodyRenderer(),
            new FooterRenderer()
        };
    }

    public string Render(Message message)
    {
        return SubRenderers
            .Select(x => x.Render(message))
            .Aggregate("", (str1, str2) => str1 + str2);
    }
}", sourceCode);
   }
}