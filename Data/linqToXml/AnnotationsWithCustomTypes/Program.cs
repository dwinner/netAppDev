using System.Xml.Linq;

namespace AnnotationsWithCustomTypes;

internal static class Program
{
   public static void Main()
   {
      var e = new XElement("test");

      e.AddAnnotation(new CustomData { Message = "Hello" });
      Console.WriteLine(e.Annotations<CustomData>().First().Message);

      e.RemoveAnnotations<CustomData>();
      Console.WriteLine(e.Annotations<CustomData>().Count());
   }
}

internal class CustomData // Private nested type
{
   internal string? Message;
}