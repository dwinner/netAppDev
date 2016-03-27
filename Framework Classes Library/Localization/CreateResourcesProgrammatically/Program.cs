/**
 * Создание ресурсов программным образом
 */

using System.Drawing;
using System.Resources;

namespace CreateResourcesProgrammatically
{
   class Program
   {
      static void Main()
      {
         using (var resXResourceWriter = new ResXResourceWriter("Demo.resx"))
         using (Image image = Image.FromFile("logo.gif"))
         {
            resXResourceWriter.AddResource("WroxLogo", image);
            resXResourceWriter.AddResource("Title", "Professional C#");
            resXResourceWriter.AddResource("Chapter", "Localization");
            resXResourceWriter.AddResource("Author", "Christian Nagel");
            resXResourceWriter.AddResource("Publisher", "Wrox Press");
         }
      }
   }
}
