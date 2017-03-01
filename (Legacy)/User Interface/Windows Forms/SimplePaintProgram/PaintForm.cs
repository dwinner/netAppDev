using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SimplePaintProgram.Properties;

namespace SimplePaintProgram
{
   public partial class PaintForm : Form
   {
      private const float DefaultBoundedHeight = 20;
      private const float DefaultBoundedWidth = 20;
      private readonly List<ShapeData> _shapes = new List<ShapeData>();
      private Color _currentColor = Color.DarkBlue;
      private SelectedShape _currentShape;

      public PaintForm()
      {
         InitializeComponent();
      }

      private void OnSave(object sender, EventArgs e)
      {
         using (
            var saveFileDialog = new SaveFileDialog
            {
               InitialDirectory = ".",
               Filter = Resources.ShapeFiles,
               RestoreDirectory = true,
               FileName = "My shapes"
            })
         {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
               using (var openFileStream = saveFileDialog.OpenFile())
               {
                  var binaryFormatter = new BinaryFormatter();
                  binaryFormatter.Serialize(openFileStream, _shapes);
               }
            }
         }
      }

      private void OnLoad(object sender, EventArgs e)
      {
         using (
            var openFileDialog = new OpenFileDialog
            {
               InitialDirectory = ".",
               Filter = Resources.ShapeFiles,
               RestoreDirectory = true,
               FileName = "My shapes"
            })
         {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
               var openFileStream = openFileDialog.OpenFile();
               var binaryFormatter = new BinaryFormatter();
               _shapes.Clear();
               var shapeData = binaryFormatter.Deserialize(openFileStream) as List<ShapeData>;
               if (shapeData != null)
               {
                  _shapes.AddRange(shapeData);
                  Invalidate();
               }
            }
         }
      }

      private void OnExit(object sender, EventArgs e)
      {
         Close();
      }

      private void OnPickShapes(object sender, EventArgs e)
      {
         var pickerDialog = new ShapePickerDialog();
         if (pickerDialog.ShowDialog() == DialogResult.OK)
         {
            _currentShape = pickerDialog.SelectedShape;
         }
      }

      private void OnPickColor(object sender, EventArgs e)
      {
         using (var colorDialog = new ColorDialog())
         {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
               _currentColor = colorDialog.Color;
            }
         }
      }

      private void OnClearSurface(object sender, EventArgs e)
      {
         _shapes.Clear();
         Invalidate();
      }

      private void PaintForm_MouseDown(object sender, MouseEventArgs e)
      {
         _shapes.Add(new ShapeData
         {
            SelectedShape = _currentShape,
            Color = _currentColor,
            UpperLeftPoint = new Point(e.X, e.Y)
         });
         Invalidate();
      }

      private void PaintForm_Paint(object sender, PaintEventArgs e)
      {
         var graphics = e.Graphics;
         foreach (var shapeData in _shapes)
         {
            if (shapeData.SelectedShape == SelectedShape.Rectangle)
            {
               graphics.FillRectangle(new SolidBrush(shapeData.Color), shapeData.UpperLeftPoint.X,
                  shapeData.UpperLeftPoint.Y, DefaultBoundedWidth, DefaultBoundedHeight);
            }
            else
            {
               graphics.FillEllipse(new SolidBrush(shapeData.Color), shapeData.UpperLeftPoint.X,
                  shapeData.UpperLeftPoint.Y, DefaultBoundedWidth, DefaultBoundedHeight);
            }
         }
      }
   }
}