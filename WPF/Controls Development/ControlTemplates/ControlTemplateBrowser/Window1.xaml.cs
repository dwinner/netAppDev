using System;
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
         var controlType = typeof(Control);

         // Search all the types in the assembly where the Control class is defined.
         var assembly = Assembly.GetAssembly(typeof(Control));
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
            var selectedTypeItem = (Type)TypesListbox.SelectedItem;

            // Instantiate the type.
            var defaultCtorInfo = selectedTypeItem.GetConstructor(Type.EmptyTypes);
            if (defaultCtorInfo == null)
               return;

            var control = (Control)defaultCtorInfo.Invoke(null);
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
            var settings = new XmlWriterSettings { Indent = true };
            var xamlStrBuilder = new StringBuilder();
            var writer = XmlWriter.Create(xamlStrBuilder, settings);
            XamlWriter.Save(template, writer);

            // Display the template.
            TemplateTextbox.Text = xamlStrBuilder.ToString();

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
            TemplateTextbox.Text = string.Format("<< Error generating template: {0}>>", err.Message);
         }
      }
   }
}