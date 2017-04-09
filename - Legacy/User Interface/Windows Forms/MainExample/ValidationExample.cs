using System.ComponentModel;
using System.Windows.Forms;

namespace MainExample
{
   public partial class ValidationExample : Form
   {
      public ValidationExample()
      {
         InitializeComponent();
      }

      private void ageText_Validating(object sender, CancelEventArgs e)
      {
         // Get the text from the age control and validate it
         int age;
         var valid = false;

         if (int.TryParse(ageText.Text, out age))
         {
            // Now check the age
            if ((age > 0) && (age < 66))
               valid = true;
         }

         errorProvider1.SetError(ageText, valid ? string.Empty : "The age must be numeric and between 1 and 65");
      }
   }
}