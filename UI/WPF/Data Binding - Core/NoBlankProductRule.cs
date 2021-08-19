using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using StoreDatabase;

namespace DataBinding
{
   public class NoBlankProductRule : ValidationRule
   {
      public override ValidationResult Validate(object value, CultureInfo cultureInfo)
      {
         var bindingGroup = (BindingGroup) value;

         // This product has the original values.
         if (bindingGroup == null)
            return new ValidationResult(true, null);

         var product = (Product) bindingGroup.Items[0];
         
         // Check the new values.
         var newModelName = (string) bindingGroup.GetValue(product, nameof(Product.ModelName));
         var newModelNumber = (string) bindingGroup.GetValue(product, nameof(Product.ModelNumber));

         return newModelName == string.Empty && newModelNumber == string.Empty
            ? new ValidationResult(false, "A product requires a ModelName or ModelNumber.")
            : new ValidationResult(true, null);
      }
   }
}