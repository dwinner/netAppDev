using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManagersHolidayRequestApprovalApplication
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
                HolidayRequestService.HolidayApprovalDataContract_Input input;
                input = new HolidayRequestService.HolidayApprovalDataContract_Input();
                input.ManagerID = 1;
                input.ReferenceID = int.Parse(textBox2.Text);
                input.ApprovedOrDenied = HolidayRequestService.ApprovedOrDeniedEnum.Approved;

                HolidayRequestService.HolidayApprovalDataContract_Output output;
                output = client.ApproveRequest(input);

                //MessageBox.Show(output.EmployeeID.ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
                HolidayRequestService.ServiceClient client;
                client = new HolidayRequestService.ServiceClient();
                HolidayRequestService.HolidayApprovalDataContract_Input input;
                input = new HolidayRequestService.HolidayApprovalDataContract_Input();
                input.ManagerID = 1;
                input.ReferenceID = int.Parse(textBox2.Text);
                input.ApprovedOrDenied = HolidayRequestService.ApprovedOrDeniedEnum.Denied;

                HolidayRequestService.HolidayApprovalDataContract_Output output;
                output = client.ApproveRequest(input);

                //MessageBox.Show(output.EmployeeID.ToString());

          
        }
    }
}
