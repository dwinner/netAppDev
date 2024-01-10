using System.Windows;
using Microsoft.Scripting.Hosting;

namespace DlrHostSample;

public partial class MainWindow
{
   public MainWindow()
   {
      InitializeComponent();
   }

   private void button1_Click(object sender, RoutedEventArgs e)
   {
      var scriptToUse = CostRadioButton.IsChecked.Value ? "AmountDisc.py" : "CountDisc.py";
      var scriptRuntime = ScriptRuntime.CreateFromConfiguration();
      var rbEng = scriptRuntime.GetEngine("python");
      var source = rbEng.CreateScriptSourceFromFile(scriptToUse);
      var scope = rbEng.CreateScope();
      scope.SetVariable("prodCount", Convert.ToInt32(totalItems.Text));
      scope.SetVariable("amt", Convert.ToDecimal(totalAmt.Text));
      source.Execute(scope);
      label5.Content = scope.GetVariable("retAmt").ToString();
   }

   private void button2_Click(object sender, RoutedEventArgs e)
   {
      var scriptRuntime = ScriptRuntime.CreateFromConfiguration();
      dynamic calcRate = scriptRuntime.UseFile("CalcTax.py");
      label6.Content = calcRate.CalcTax(Convert.ToDecimal(label5.Content)).ToString();
   }
}