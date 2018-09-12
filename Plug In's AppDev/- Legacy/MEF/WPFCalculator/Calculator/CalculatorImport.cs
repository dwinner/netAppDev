using System;
using System.ComponentModel.Composition;
using CalculatorContract;
using CalculatorUtils;

namespace Calculator
{
   public class CalculatorImport : IPartImportsSatisfiedNotification
   {
      [Import(typeof (ICalculator))]
      public Lazy<ICalculator> Calculator { get; set; }

      public void OnImportsSatisfied()
      {
         OnImportsSatisfied(new ImportEventArgs {StatusMessage = "ICalculator import successful"});
      }

      public event EventHandler<ImportEventArgs> ImportsSatisfied;

      protected virtual void OnImportsSatisfied(ImportEventArgs e)
      {
         EventHandler<ImportEventArgs> handler = ImportsSatisfied;
         if (handler != null) handler(this, e);
      }
   }
}