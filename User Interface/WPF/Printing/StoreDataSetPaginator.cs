using System;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Printing
{
   internal sealed class StoreDataSetPaginator : DocumentPaginator
   {
      private readonly DataTable _dataTable;
      private readonly double _fontSize;
      private readonly double _margin;
      private readonly Typeface _typeface;
      private int _pageCount;
      private Size _pageSize;      
      private int _rowsPerPage;

      public StoreDataSetPaginator(DataTable dataTable, Typeface typeface, double fontSize, double margin, Size pageSize)
      {
         _dataTable = dataTable;
         _typeface = typeface;
         _fontSize = fontSize;
         _margin = margin;
         _pageSize = pageSize;
         PaginateData();
      }

      public override bool IsPageCountValid
      {
         get { return true; }
      }

      public override int PageCount
      {
         get { return _pageCount; }
      }

      public override Size PageSize
      {
         get { return _pageSize; }
         set
         {
            _pageSize = value;
            PaginateData();
         }
      }

      public override IDocumentPaginatorSource Source
      {
         get { return null; }
      }

      private void PaginateData()
      {
         // Создать тестовую строку для измерения.
         var text = GetFormattedText("A");   // NOTE: Здесь ограничимся символом A, однако в некоторых случаях этого может быть недостаточно

         // Подсчитать строки, которые умещаются на странице.
         _rowsPerPage = (int) ((_pageSize.Height - _margin * 2) / text.Height);

         // Оставить строку для заголовка
         _rowsPerPage -= 1;

         _pageCount = (int) Math.Ceiling((double) _dataTable.Rows.Count / _rowsPerPage);
      }

      public override DocumentPage GetPage(int pageNumber)
      {
         // Создать тестовую строку для измерения.
         var text = GetFormattedText("A");         
         var col1X = _margin;
         var col2X = col1X + text.Width * 15;

         // Вычислить диапазон строк, которые попадают в эту страницу.
         var minRow = pageNumber * _rowsPerPage;
         var maxRow = minRow + _rowsPerPage;

         // Создать визуальный объект для страницы.
         var visual = new DrawingVisual();

         // Установить позицию в верхний левый угол печатаемой области.
         var point = new Point(_margin, _margin);
         
         using (var context = visual.RenderOpen())
         {
            // Нарисовать заголовки столбцов.
            var columnHeaderTypeface = new Typeface(_typeface.FontFamily, FontStyles.Normal, FontWeights.Bold,
               FontStretches.Normal);
            point.X = col1X;
            text = GetFormattedText("Model Number", columnHeaderTypeface);
            context.DrawText(text, point);
            text = GetFormattedText("Model Name", columnHeaderTypeface);
            point.X = col2X;
            context.DrawText(text, point);

            // Нарисовать линию подчеркивания.
            context.DrawLine(
               new Pen(Brushes.Black, 2),
               new Point(_margin, _margin + text.Height),
               new Point(_pageSize.Width - _margin, _margin + text.Height));

            point.Y += text.Height;

            // Нарисовать значения стоблцов.
            for (var rowIndex = minRow; rowIndex < maxRow; rowIndex++)
            {
               // Проверить конец последней (частично заполненной) страницы.
               if (rowIndex > _dataTable.Rows.Count - 1)
                  break;

               point.X = col1X;
               text = GetFormattedText(_dataTable.Rows[rowIndex]["ModelNumber"].ToString());
               context.DrawText(text, point);

               // Добавить второй столбец.                    
               text = GetFormattedText(_dataTable.Rows[rowIndex]["ModelName"].ToString());
               point.X = col2X;
               context.DrawText(text, point);
               point.Y += text.Height;
            }
         }

         return new DocumentPage(visual, _pageSize, new Rect(_pageSize), new Rect(_pageSize));
      }


      private FormattedText GetFormattedText(string text)
      {
         return GetFormattedText(text, _typeface);
      }

      private FormattedText GetFormattedText(string text, Typeface typeface)
      {
         return new FormattedText(
            text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
            typeface, _fontSize, Brushes.Black);
      }
   }
}