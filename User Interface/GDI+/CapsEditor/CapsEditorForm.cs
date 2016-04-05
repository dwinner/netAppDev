using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CapsEditor.Properties;

namespace CapsEditor
{
   public partial class CapsEditorForm : Form
   {
      private const string StandartTitle = "CapsEditor"; // Текст заголовка по умолчанию
      private const uint StandartMargin = 10; // горизонтальное и вертикальное поля отступов клиентской области
      private readonly List<TextLineInformation> _documentLines = new List<TextLineInformation>(); // Документ
      private readonly Brush _emptyDocumentBrush = Brushes.Red; // Кисть, используемая для отображения пустого сообщения
      private readonly OpenFileDialog _fileOpenDialog = new OpenFileDialog { Filter = Resources.FileFilterPattern };
      // Стандартное диалоговое окно открытия файла

      private readonly Brush _mainBrush = Brushes.Blue; // Кисть, используемая для отображения текста документа
      private bool _documentHasData; // Устанавливается в true, если документ содержит данные
      private Size _documentSize; // Необходимый размер клиентской области для отображения всего документа
      private Font _emptyDocumentFont; // Шрифт, используемый для отображения пустого сообщения
      private uint _lineHeight; // Высота одной строки в пикселях
      private Font _mainFont; // Шрифт, используемый для отображения строк документа
      private Point _mouseDoubleClickPosition; // Положение указателя мыши при двойном щелчке
      private uint _nLines; // Кол-во строк в документе
      private int _pagesPrinted;

      public CapsEditorForm()
      {
         InitializeComponent();
         CreateFonts();
         _fileOpenDialog.FileOk += (sender, args) => { LoadFile(_fileOpenDialog.FileName); };
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);
         var dc = e.Graphics;
         var scrollPositionX = AutoScrollPosition.X;
         var scrollPositionY = AutoScrollPosition.Y;
         dc.TranslateTransform(scrollPositionX, scrollPositionY);
         if (!_documentHasData)
         {
            dc.DrawString("<Empty document>", _emptyDocumentFont, _emptyDocumentBrush, new Point(20, 20));
            base.OnPaint(e);
            return;
         }

         // Поиск строк, находящихся в области отсечения
         var minLineInClipRegion = WorldYCoordinateToLineIndex(e.ClipRectangle.Top - scrollPositionY);
         if (minLineInClipRegion == -1)
            minLineInClipRegion = 0;
         var maxLineInClipRegion = WorldYCoordinateToLineIndex(e.ClipRectangle.Bottom - scrollPositionY);
         if (maxLineInClipRegion >= _documentLines.Count || maxLineInClipRegion == -1)
            maxLineInClipRegion = _documentLines.Count - 1;
         for (var i = minLineInClipRegion; i <= maxLineInClipRegion; i++)
         {
            var nextLine = _documentLines[i];
            dc.DrawString(nextLine.Text, _mainFont, _mainBrush, LineIndexToWorldCoordinates(i));
         }
      }

      protected override void OnMouseDown(MouseEventArgs e)
      {
         base.OnMouseDown(e);
         _mouseDoubleClickPosition = new Point(e.X, e.Y);
      }

      protected override void OnDoubleClick(EventArgs e)
      {
         var i = PageCoordinatesToLineIndex(_mouseDoubleClickPosition);
         if (i >= 0)
         {
            var lineToBeChanged = _documentLines[i];
            lineToBeChanged.Text = lineToBeChanged.Text.ToUpper();
            var dc = CreateGraphics();
            var newWidth = (uint)dc.MeasureString(lineToBeChanged.Text, _mainFont).Width;
            if (newWidth > lineToBeChanged.Width)
               lineToBeChanged.Width = newWidth;
            if (newWidth + 2 * StandartMargin > _documentSize.Width)
            {
               _documentSize.Width = (int)newWidth;
               AutoScrollMinSize = _documentSize;
            }

            var changedRectangle = new Rectangle(LineIndexToPageCoordinates(i),
               new Size((int)newWidth, (int)_lineHeight));
            Invalidate(changedRectangle);
         }

         base.OnDoubleClick(e);
      }

      private int WorldYCoordinateToLineIndex(int y)
      {
         return y < StandartMargin ? -1 : (int)((y - StandartMargin) / _lineHeight);
      }

      private int WorldCoordinateToLineIndex(Point position)
      {
         if (!_documentHasData)
            return -1;
         if (position.Y < StandartMargin || position.X < StandartMargin)
            return -1;
         var index = (int)(position.Y - StandartMargin) / (int)_lineHeight;
         // Если позиция за пределами документа
         if (index >= _documentLines.Count)
            return -1;
         // Проверить, что позиция по горизонтали находится в пределах строки
         var theLine = _documentLines[index];
         if (position.X > StandartMargin + theLine.Width)
            return -1;
         // Все в порядке, можно вернуть номер строки
         return index;
      }

      private Point LineIndexToPageCoordinates(int index)
      {
         return LineIndexToWorldCoordinates(index) + new Size(AutoScrollPosition);
      }

      private int PageCoordinatesToLineIndex(Point position)
      {
         return WorldCoordinateToLineIndex(position - new Size(AutoScrollPosition));
      }

      private Point LineIndexToWorldCoordinates(int index)
      {
         var topLeftCorner = new Point((int)StandartMargin, (int)(_lineHeight * index + StandartMargin));
         return topLeftCorner;
      }

      private void LoadFile(string fileName)
      {
         using (var streamReader = new StreamReader(fileName))
         {
            string nextLine;
            _documentLines.Clear();
            _nLines = 0;
            while ((nextLine = streamReader.ReadLine()) != null)
            {
               _documentLines.Add(new TextLineInformation { Text = nextLine });
               ++_nLines;
            }
         }

         PrintPreviewToolStripMenuItem.Enabled = PrintToolStripMenuItem.Enabled = _documentHasData = _nLines > 0;
         CalculateLineWidths();
         CalculateDocumentSize();
         Text = string.Format("{0} - {1}", StandartTitle, fileName);
         Invalidate(); // NOTE: В данном частном случае этот метод избыточен
      }

      private void CalculateDocumentSize()
      {
         if (!_documentHasData)
         {
            _documentSize = new Size(100, 200);
         }
         else
         {
            _documentSize.Height = (int)(_nLines * _lineHeight) + 2 * (int)StandartMargin;
            uint[] maxLineLength = { 0 };
            foreach (
               var tempLineLength in
                  _documentLines.Select(nextWord => nextWord.Width)
                     .Where(tempLineLength => tempLineLength > maxLineLength[0]))
            {
               maxLineLength[0] = tempLineLength;
            }

            maxLineLength[0] += 2 * StandartMargin;
            _documentSize.Width = (int)maxLineLength[0];
         }

         AutoScrollMinSize = _documentSize; // NOTE: Произойдет событие перерисовки
      }

      private void CalculateLineWidths()
      {
         var graphics = CreateGraphics();
         foreach (var nextLine in _documentLines)
         {
            nextLine.Width = (uint)graphics.MeasureString(nextLine.Text, _mainFont).Width;
         }
      }

      private void CreateFonts()
      {
         _mainFont = new Font("Arial", 10);
         _lineHeight = (uint)_mainFont.Height;
         _emptyDocumentFont = new Font("Verdana", 13, FontStyle.Bold);
      }

      private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
      {
         _fileOpenDialog.ShowDialog();
      }

      private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
      {
         Close();
      }

      private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
      {
         _pagesPrinted = 0;
         using (var printDocument = new PrintDocument())
         {
            printDocument.PrintPage += OnPrintPage;
            printDocument.Print();
         }
      }

      private void PrintPreviewToolStripMenuItem_Click(object sender, EventArgs e)
      {
         _pagesPrinted = 0;
         using (var previewDialog = new PrintPreviewDialog())
         using (var printDocument = new PrintDocument())
         {
            printDocument.PrintPage += OnPrintPage;
            previewDialog.Document = printDocument;
            previewDialog.ShowDialog();
         }
      }

      private void OnPrintPage(object sender, PrintPageEventArgs e)
      {
         float leftMargin = e.MarginBounds.Left;
         float topMargin = e.MarginBounds.Top;
         // Вычислить кол-во строк на страницу
         var linesPerPage = (int)(e.MarginBounds.Height / _mainFont.GetHeight(e.Graphics));
         var lineNo = _pagesPrinted * linesPerPage;
         // Печатать каждую строку файла
         var count = 0;
         while (count < linesPerPage && lineNo < _nLines)
         {
            var line = _documentLines[lineNo].Text;
            var yPos = topMargin + (count * _mainFont.GetHeight(e.Graphics));
            e.Graphics.DrawString(line, _mainFont, Brushes.Blue, leftMargin, yPos, new StringFormat());
            lineNo++;
            count++;
         }

         // Если еще остались строки, печатать другую страницу
         e.HasMorePages = _nLines > lineNo;
         _pagesPrinted++;
      }

      private class TextLineInformation
      {
         public string Text { get; set; }
         public uint Width { get; set; }
      }
   }
}