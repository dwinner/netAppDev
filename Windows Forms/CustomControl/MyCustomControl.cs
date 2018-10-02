using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CustomControl
{
   public enum RGBSelection
   {
      R,
      G,
      B
   };

   public partial class MyCustomControl : UserControl
   {
      //the controls
      private readonly IContainer components = null;
      private Label labelLabel;
      private NumericUpDown numericUpDownValue;
      private TrackBar trackBarValue;

      public MyCustomControl()
      {
         InitializeComponent();

         numericUpDownValue.Minimum = 0;
         numericUpDownValue.Maximum = 255;
         trackBarValue.Minimum = 0;
         trackBarValue.Maximum = 255;

         trackBarValue.TickFrequency = 10;

         numericUpDownValue.ValueChanged += numericUpDownValue_ValueChanged;
         trackBarValue.ValueChanged += trackBarValue_ValueChanged;
      }

      public RGBSelection ColorPart { get; set; }

      public string Label
      {
         get { return labelLabel.Text; }
         set { labelLabel.Text = value; }
      }

      public int Value
      {
         get { return (int) numericUpDownValue.Value; }
         set { numericUpDownValue.Value = value; }
      }

      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
         labelLabel = new Label();
         numericUpDownValue = new NumericUpDown();
         trackBarValue = new TrackBar();
         ((ISupportInitialize) (numericUpDownValue)).BeginInit();
         ((ISupportInitialize) (trackBarValue)).BeginInit();
         SuspendLayout();
         // labelLabel
         labelLabel.AutoSize = true;
         labelLabel.Location = new Point(4, 4);
         labelLabel.Name = "labelLabel";
         labelLabel.Size = new Size(71, 13);
         labelLabel.TabIndex = 0;
         labelLabel.Text = "Dummy Label";
         // numericUpDownValue
         numericUpDownValue.Location = new Point(175, 20);
         numericUpDownValue.Name = "numericUpDownValue";
         numericUpDownValue.Size = new Size(41, 20);
         numericUpDownValue.TabIndex = 1;
         // trackBarValue
         trackBarValue.Location = new Point(7, 20);
         trackBarValue.Name = "trackBarValue";
         trackBarValue.Size = new Size(162, 45);
         trackBarValue.TabIndex = 2;
         // MyCustomControl
         AutoScaleDimensions = new SizeF(6F, 13F);
         AutoScaleMode = AutoScaleMode.Font;
         Controls.Add(trackBarValue);
         Controls.Add(numericUpDownValue);
         Controls.Add(labelLabel);
         Name = "MyCustomControl";
         Size = new Size(216, 65);
         ((ISupportInitialize) (numericUpDownValue)).EndInit();
         ((ISupportInitialize) (trackBarValue)).EndInit();
         ResumeLayout(false);
         PerformLayout();
      }

      public event EventHandler<EventArgs> ValueChanged;

      private void trackBarValue_ValueChanged(object sender, EventArgs e)
      {
         if (sender != this)
         {
            numericUpDownValue.Value = trackBarValue.Value;

            OnValueChanged();
         }
      }

      private void numericUpDownValue_ValueChanged(object sender, EventArgs e)
      {
         if (sender != this)
         {
            trackBarValue.Value = (int) numericUpDownValue.Value;

            OnValueChanged();
         }
      }

      protected void OnValueChanged()
      {
         Refresh();
         if (ValueChanged != null)
         {
            ValueChanged(this, EventArgs.Empty);
         }
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);

         var rect = new Rectangle(numericUpDownValue.Left, 5, numericUpDownValue.Width,
            numericUpDownValue.Bounds.Top - 10);

         int r = 0, g = 0, b = 0;
         switch (ColorPart)
         {
            case RGBSelection.R:
               r = Value;
               break;
            case RGBSelection.G:
               g = Value;
               break;
            case RGBSelection.B:
               b = Value;
               break;
         }
         var c = Color.FromArgb(r, g, b);
         using (Brush brush = new SolidBrush(c))
         {
            e.Graphics.FillEllipse(brush, rect);
         }
      }
   }
}