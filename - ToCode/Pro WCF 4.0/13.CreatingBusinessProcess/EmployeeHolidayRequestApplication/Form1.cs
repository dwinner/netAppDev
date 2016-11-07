using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmployeeHolidayRequestApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            HolidayRequestService.ServiceClient client;
            client = new HolidayRequestService.ServiceClient();
            HolidayRequestService.HolidayRequestDataContract_Output output;
            output = client.RequestHoliday(new HolidayRequestService.HolidayRequestDataContract_Input()
            {
                EmployeeID = 101,
                HolidayEndDate = System.DateTime.Now,
                HolidayStartDate = System.DateTime.Now
            });
            MessageBox.Show(output.ReferenceID.ToString());

        }
    }
}
