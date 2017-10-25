using System;
using System.Windows.Forms;

namespace SimplePaintProgram
{
   public partial class ShapePickerDialog : Form
   {
      private const SelectedShape DefaultSelectedShape = SelectedShape.Circle;

      public ShapePickerDialog()
      {
         InitializeComponent();
      }

      public SelectedShape SelectedShape { get; private set; }

      private void OkBtn_Click(object sender, EventArgs e)
      {
         SelectedShape = CircleRadioButton.Checked
            ? SelectedShape.Circle
            : (RectangleRadioButton.Checked ? SelectedShape.Rectangle : DefaultSelectedShape);
      }      
   }
}