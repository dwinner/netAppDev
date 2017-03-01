using System;
using System.Drawing;
using System.Windows.Forms;
using TransparentForm.Properties;

namespace TransparentForm
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
         FormBorderStyle = FormBorderStyle.None;
         TransparencyKey = Color.White;
         Size = Resources.WindowTemplate.Size;
         MouseDown += Form1_MouseDown;
      }

      private void Form1_MouseDown(object sender, MouseEventArgs e)
      {
         //if we click anywhere on the form itself (not a child control),
         //then tell Windows we're clicking on the non-client area
         Capture = false;
         Win32.SendMessage(Handle, Win32.WmNclbuttondown, Win32.Htcaption, 0);
      }

      private void buttonClose_Click(object sender, EventArgs e)
      {
         Application.Exit();
      }
   }
}