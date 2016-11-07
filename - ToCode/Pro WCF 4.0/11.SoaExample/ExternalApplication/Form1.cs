using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace ExternalApplication
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      try
      {
        ExternalClientProxy proxy;
        proxy = new ExternalClientProxy();
        ExternalInterface.RentalContract rentalContract;
        rentalContract = new ExternalInterface.RentalContract();
        rentalContract.Company = "XXXX";
        rentalContract.CompanyReferenceID = "YYYY";
        rentalContract.RentalRegistration = new RentalInterface.RentalRegistration();
        rentalContract.RentalRegistration.PickUpLocation = 111;
        rentalContract.Customer = new CustomerInterface.Customer();
        rentalContract.Customer.CustomerName = "AAAAA";
        rentalContract.Customer.CustomerFirstName = "BBBB";

        proxy.SubmitRentalContract(rentalContract);
      }
      catch (Exception ex)
      {

        MessageBox.Show(ex.Message);
      }
    }
  }
}
