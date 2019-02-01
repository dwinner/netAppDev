using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using mshtml;

namespace WebBrowserTest
{
   public partial class BrowseDom
   {
      public BrowseDom()
      {
         InitializeComponent();
      }

      private void OnAnalyzeDom(object sender, RoutedEventArgs e)
      {
         OnBuildTree();
      }

      private void OnBuildTree()
      {
         // Analyzing a page takes a nontrivial amount of time.
         // Use the hourglass cursor to warn the user.
         Cursor = Cursors.Wait;

         var dom = (HTMLDocument)WebBrowser.Document;

         // Process all the HTML elements on the page.
         ProcessElement(dom.documentElement, TreeDom.Items);

         Cursor = null;
      }

      private static void ProcessElement(IHTMLElement parentElement, IList nodes)
      {
         // Scan through the collection of elements.
         foreach (IHTMLElement element in parentElement.children)
         {
            // Create a new node that shows the tag name.
            var node = new TreeViewItem { Header = string.Format("<{0}>", element.tagName) };
            nodes.Add(node);

            if (element.children.length == 0 && element.innerText != null)
               node.Items.Add(element.innerText);
            else
               ProcessElement(element, node.Items);
         }
      }
   }
}