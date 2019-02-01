using CommentRemover.Extensions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommentRemover.Tests
{
   [TestClass]
   public class SyntaxNodeExtensionsTests
   {
      [TestMethod]
      public void RemoveCommentsFromCodeWithMultiLineComments()
      {
         const string code = @"public class Class
{
    public void Method()
    {
        /*
         Here is a multiline comment.
        */
    }
}";
         const string newCode = @"public class Class
{
    public void Method()
    {
    }
}";
         var compilation = SyntaxFactory.ParseCompilationUnit(code);
         var newCompilation = compilation.RemoveComments();

         Assert.AreNotSame(compilation, newCompilation);
         var actual = newCompilation.GetText().ToString();
         Assert.AreEqual(newCode, actual);
      }

      [TestMethod]
      public void RemoveCommentsFromCodeWithSingleLineComments()
      {
         const string code = @"public class Class
{
	public void Method()
	{
		// Here is a single line comment
		// Here is another single line comment
	}
}";

         const string newCode = @"public class Class
{
	public void Method()
	{
	}
}";
         var node = SyntaxFactory.ParseCompilationUnit(code);
         var newNode = node.RemoveComments();

         Assert.AreNotSame(node, newNode);
         Assert.AreEqual(newCode, newNode.GetText().ToString());
      }

      [TestMethod]
      public void RemoveCommentsFromCodeWithSingleLineCommentsWithoutWhitespace()
      {
         const string code = @"public class Class
{
	public void Method()
	{
// Here is a single line comment
	}
}";

         const string newCode = @"public class Class
{
	public void Method()
	{
	}
}";
         var node = SyntaxFactory.ParseCompilationUnit(code);
         var newNode = node.RemoveComments();

         Assert.AreNotSame(node, newNode);
         Assert.AreEqual(newCode, newNode.GetText().ToString());
      }

      [TestMethod]
      public void RemoveCommentsFromCodeWithMultiLineDocumentationComments()
      {
         const string code = @"public class Class
{
	/// <summary>
	/// Here are some XML comments
	/// </summary>
	public void Method()
	{
	}
}";

         var node = SyntaxFactory.ParseCompilationUnit(code);
         var newNode = node.RemoveComments();

         Assert.AreSame(node, newNode);
      }
   }
}