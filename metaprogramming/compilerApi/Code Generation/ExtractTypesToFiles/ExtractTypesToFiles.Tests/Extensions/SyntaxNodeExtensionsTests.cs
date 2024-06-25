using System.IO;
using System.Threading.Tasks;
using ExtractTypesToFiles.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtractTypesToFiles.Tests.Extensions
{
   [TestClass]
   public sealed class SyntaxNodeExtensionsTests
   {
      [TestMethod]
      public async Task GenerateUsingDirectives()
      {
         const string folder = nameof(SyntaxNodeExtensionsTests);
         const string fileName = nameof(GenerateUsingDirectives);

         var document = TestHelpers.CreateDocument(
            File.ReadAllText($@"Targets\Extensions\{folder}\{fileName}.cs"),
            "Class1.cs", null);

         var root = await document.GetSyntaxRootAsync().ConfigureAwait(false);
         var model = await document.GetSemanticModelAsync().ConfigureAwait(false);

         var directives = root.GenerateUsingDirectives(model);
         Assert.AreEqual(3, directives.Count);
         //directives.Single(_ => _.Name.ToString() == "System");
         //directives.Single(_ => _.Name.ToString() == "System.IO");
         //directives.Single(_ => _.Name.ToString() == "System.Text");
      }
   }
}