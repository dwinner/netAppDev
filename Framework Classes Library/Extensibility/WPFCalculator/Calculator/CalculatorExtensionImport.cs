using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using CalculatorContract;
using CalculatorUtils;

namespace Calculator
{
   public class CalculatorExtensionImport : IPartImportsSatisfiedNotification
   {
      [ImportMany(AllowRecomposition = true)]
      public IEnumerable<Lazy<ICalculatorExtension, ICalculatorExtensionMetadata>> CalculatorExtensions { get; set; }

      public void OnImportsSatisfied()
      {
         OnImportsSatisfied(new ImportEventArgs {StatusMessage = "ICalculatorExtension imports successful"});
      }

      public event EventHandler<ImportEventArgs> ImportsSatisfied;

      protected virtual void OnImportsSatisfied(ImportEventArgs e)
      {
         EventHandler<ImportEventArgs> handler = ImportsSatisfied;
         if (handler != null) handler(this, e);
      }
   }
}