//------------------------------------------------------------------------------
// <copyright file="SampleToolWindowControl.xaml.cs" company="Hewlett-Packard Company">
//     Copyright (c) Hewlett-Packard Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;

namespace ToolWindowSample
{
   /// <summary>
   ///    Interaction logic for SampleToolWindowControl.
   /// </summary>
   public partial class SampleToolWindowControl
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="SampleToolWindowControl" /> class.
      /// </summary>
      public SampleToolWindowControl()
      {
         InitializeComponent();
      }

      /// <summary>
      ///    Handles click on the button by displaying a message box.
      /// </summary>
      /// <param name="sender">The event sender.</param>
      /// <param name="e">The event args.</param>
      [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
      [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter",
         Justification = "Default event handler naming pattern")]
      private void button1_Click(object sender, RoutedEventArgs e)
      {
         MessageBox.Show(
            string.Format(CultureInfo.CurrentUICulture, "Invoked '{0}'", ToString()),
            "SampleToolWindow");
      }
   }
}