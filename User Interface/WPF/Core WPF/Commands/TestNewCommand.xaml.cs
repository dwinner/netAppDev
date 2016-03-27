using System.Windows;
using System.Windows.Input;

namespace Commands
{   
   public partial class TestNewCommand
   {
      public TestNewCommand()
      {
         //ApplicationCommands.New.Text = "Completely New";

         InitializeComponent();

         //CommandBinding bindingNew = new CommandBinding(ApplicationCommands.New);
         //bindingNew.Executed += NewCommand;

         //this.CommandBindings.Add(bindingNew);
      }

      private void NewCommand(object sender, ExecutedRoutedEventArgs e)
      {
         MessageBox.Show(string.Format("New command triggered by {0}", e.Source));
      }

      private void cmdDoCommand_Click(object sender, RoutedEventArgs e)
      {
         CommandBindings[0].Command.Execute(null);
         // ApplicationCommands.New.Execute(null, (Button)sender);
      }
   }
}