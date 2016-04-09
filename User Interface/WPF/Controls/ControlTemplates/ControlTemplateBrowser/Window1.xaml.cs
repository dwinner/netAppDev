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

      private void OnWindowLoaded(object sender, EventArgs e)
      {
         var controlType = typeof (Control);

         // Search all the types in the assembly where the Control class is defined.
         var assembly = Assembly.GetAssembly(typeof (Control));
         var derivedTypes =
            assembly.GetTypes()
               .Where(type => type.IsSubclassOf(controlType) && !type.IsAbstract && type.IsPublic)
               .ToList();

         derivedTypes.Sort(
            (firstType, secondType) => string.Compare(firstType.Name, secondType.Name, StringComparison.Ordinal));         

         // Show the list of types.
         TypesListbox.ItemsSource = derivedTypes;
      }

      private void OnTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         try
         {
            // Get the selected type.
            var type = (Type) TypesListbox.SelectedItem;

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
               // Add it to the MainGrid (but keep it hidden).
               control.Visibility = Visibility.Collapsed;
               MainGrid.Children.Add(control);
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
            TemplateTextbox.Text = sb.ToString();

            // Remove the control from the MainGrid.
            if (win != null)
            {
               win.Close();
            }
            else
            {
               MainGrid.Children.Remove(control);
            }
         }
         catch (Exception err)
         {
            TemplateTextbox.Text = "<< Error generating template: " + err.Message + ">>";
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