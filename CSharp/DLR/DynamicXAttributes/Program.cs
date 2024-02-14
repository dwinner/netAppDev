using System.Xml.Linq;

namespace DynamicXAttributes;

internal static class Program
{
   private static void Main()
   {
      var x = XElement.Parse("""<Label Text="Hello" Id="5"/>""");
      var da = x.DynamicAttributes();
      Console.WriteLine(da.Id); // 5
      da.Text = "Foo";
      Console.WriteLine(x.ToString()); // <Label Text="Foo" Id="5" />
   }
}