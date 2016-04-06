using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace ControlTemplateBrowser
{  
   public partial class Window1
   {
      public Window1()
      {
         InitializeComponent();
      }

      private void Window_Loaded(object sender, EventArgs e)
      {
         var controlType = typeof (Control);

         // Search all the types in the assembly where the Control class is defined.
         var assembly = Assembly.GetAssembly(typeof (Control));
         var derivedTypes =
            assembly.GetTypes()
               .Where(type => type.IsSubclassOf(controlType) && !type.IsAbstract && type.IsPublic)
               .ToList();

         // Sort the types by type name.
         derivedTypes.Sort(new TypeComparer());

         // Show the list of types.
         lstTypes.ItemsSource = derivedTypes;
      }

      private void lstTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         try
         {
            // Get the selected type.
            var type = (Type) lstTypes.SelectedItem;

            // Instantiate the type.
            var info = type.GetConstructor(Type.EmptyTypes);
            var control = (Control) info.Invoke(null);

            var win = control as Window;
            if (win != null)
            {
               // Create the window (but keep it minimized).
               win.WindowState = WindowState.Minimized;
               win.ShowInTaskbar = false;
               win.Show();
            }
            else
            {
               // Add it to the grid (but keep it hidden).
               control.Visibility = Visibility.Collapsed;
               grid.Children.Add(control);
            }

            // Get the template.
            var template = control.Template;

            // Get the XAML for the template.
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            var sb = new StringBuilder();
            var writer = XmlWriter.Create(sb, settings);
            XamlWriter.Save(template, writer);

            // Display the template.
            txtTemplate.Text = sb.ToString();

            // Remove the control from the grid.
            if (win != null)
            {
               win.Close();
            }
            else
            {
               grid.Children.Remove(control);
            }
         }
         catch (Exception err)
         {
            txtTemplate.Text = "<< Error generating template: " + err.Message + ">>";
         }
      }
   }

   public class TypeComparer : IComparer<Type>
   {
      public int Compare(Type x, Type y)
      {
         return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
      }
   }
}