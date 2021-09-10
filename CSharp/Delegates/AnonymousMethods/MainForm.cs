using System;
using System.Drawing;
using System.Windows.Forms;

namespace AnonymousMethods
{
   public partial class MainForm : Form
   {
      private Point _prevPt = Point.Empty;

      public MainForm()
      {
         InitializeComponent();

         MouseMove +=
            (sender, args) =>
            {
               if (_prevPt != args.Location)
               {
                  UpdateTextBoxt.AppendText(
                     string.Format("MouseMove: ({0},{1})" + Environment.NewLine, args.X, args.Y));
                  _prevPt = args.Location;
               }
            };

         MouseClick +=
            (sender, e) =>
               UpdateTextBoxt.AppendText(
                  string.Format("MouseClick: ({0},{1}) {2}" + Environment.NewLine, e.X, e.Y, e.Button));

         MouseDown += delegate { UpdateTextBoxt.AppendText(string.Format("MouseDown{0}", Environment.NewLine)); };
         MouseUp += delegate { UpdateTextBoxt.AppendText(string.Format("MouseUp{0}", Environment.NewLine)); };
      }
   }
}
