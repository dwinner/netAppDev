using System.Drawing;
using System.Windows.Forms;

namespace PaintEventApp
{
   public partial class PaintForm : Form
   {
      public PaintForm()
      {
         InitializeComponent();
      }

      private void PaintForm_Paint(object sender, PaintEventArgs e)
      {
         var graphics = e.Graphics;
         graphics.FillEllipse(Brushes.Blue, 10, 20, 150, 80); // Прорисовка окружности
         graphics.DrawString("Hello GDI+", new Font("Times New Roman", 30), Brushes.Red, 200, 200);   // Вывод строки заданным шрифтом            
         using (var pen = new Pen(Brushes.YellowGreen, 10)) // Вывод линии заданным пером
         {
            graphics.DrawLine(pen, 80, 4, 200, 200);
         }         
      }
   }
}