namespace HtmlBuilderSample;

internal class HtmlBuilder
{
   private readonly string _rootName;
   protected HtmlElement Root = new();

   public HtmlBuilder(string rootName)
   {
      _rootName = rootName;
      Root.Name = rootName;
   }

   // not fluent
   public void AddChild(string childName, string childText)
   {
      var e = new HtmlElement(childName, childText);
      Root.Elements.Add(e);
   }

   public HtmlBuilder AddChildFluent(string childName, string childText)
   {
      var e = new HtmlElement(childName, childText);
      Root.Elements.Add(e);
      return this;
   }

   public override string ToString() => Root.ToString();

   public void Clear()
   {
      Root = new HtmlElement { Name = _rootName };
   }

   public HtmlElement Build() => Root;

   public static implicit operator HtmlElement(HtmlBuilder builder) => builder.Root;
}