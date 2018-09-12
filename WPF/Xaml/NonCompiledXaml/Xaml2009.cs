using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.IO;

namespace NonCompiledXaml
{
   public class Xaml2009Window : Window
   {
      public static Xaml2009Window LoadWindowFromXaml(string xamlFile)
      {
         // Get the XAML content from an external file.            
         using (var fileStream = new FileStream(xamlFile, FileMode.Open))
         {
            var window = (Xaml2009Window)XamlReader.Load(fileStream);
            return window;
         }
      }

      private void lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         MessageBox.Show(e.AddedItems[0].ToString());
      }
   }

   public class Person
   {
      public string FirstName { get; set; }
      public string LastName { get; set; }

      public Person(string firstName, string lastName)
      {
         FirstName = firstName;
         LastName = lastName;
      }

      public override string ToString()
      {
         return string.Format("{0} {1}", FirstName, LastName);
      }
   }

}