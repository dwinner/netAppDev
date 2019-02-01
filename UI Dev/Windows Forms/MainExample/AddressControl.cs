using System.ComponentModel;
using System.Windows.Forms;

namespace MainExample
{
   public partial class AddressControl : UserControl
   {
      public AddressControl()
      {
         InitializeComponent();
      }

      public string AddressLine1
      {
         get { return addressLine1.Text; }
         set { addressLine1.Text = value; }
      }

      public string AddressLine2
      {
         get { return addressLine2.Text; }
         set { addressLine2.Text = value; }
      }

      public string AddressLine3
      {
         get { return addressLine3.Text; }
         set { addressLine3.Text = value; }
      }

      public string AddressLine4
      {
         get { return addressLine4.Text; }
         set { addressLine4.Text = value; }
      }

      public string Postcode
      {
         get { return postcode.Text; }
         set { postcode.Text = value; }
      }

      private void OnValidateAddress1(object sender, CancelEventArgs e)
      {
         if (string.IsNullOrEmpty(addressLine1.Text))
            addressErrorProvider.SetError(addressLine1, "Address Line 1 is mandatory");
      }

      private void OnValidateAddress2(object sender, CancelEventArgs e)
      {
         if (string.IsNullOrEmpty(addressLine2.Text))
            addressErrorProvider.SetError(addressLine2, "Address Line 2 is mandatory");
      }

      private void OnValidatePostcode(object sender, CancelEventArgs e)
      {
         if (string.IsNullOrEmpty(postcode.Text))
            addressErrorProvider.SetError(postcode, "The postcode is mandatory");
      }

      private bool IsValid(Control aControl)
      {
         return !string.IsNullOrEmpty(aControl.Text);
      }
   }
}