using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace TableDemo
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();

         var doc = new FlowDocument();
         var t1 = new Table();
         t1.Columns.Add(new TableColumn {Width = new GridLength(50, GridUnitType.Pixel)});
         t1.Columns.Add(new TableColumn {Width = new GridLength(1, GridUnitType.Auto)});
         t1.Columns.Add(new TableColumn {Width = new GridLength(1, GridUnitType.Auto)});

         var titleRow = new TableRow {Background = Brushes.LightBlue};

         var titleCell = new TableCell {ColumnSpan = 3, TextAlignment = TextAlignment.Center};
         titleCell.Blocks.Add(
            new Paragraph(new Run("Formula 1 Championship 2011") {FontSize = 24, FontWeight = FontWeights.Bold}));
         titleRow.Cells.Add(titleCell);

         var headerRow = new TableRow {Background = Brushes.LightGoldenrodYellow};
         headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Pos")) {FontSize = 14, FontWeight = FontWeights.Bold}));
         headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Name")) {FontSize = 14, FontWeight = FontWeights.Bold}));
         headerRow.Cells.Add(
            new TableCell(new Paragraph(new Run("Points")) {FontSize = 14, FontWeight = FontWeights.Bold}));

         var rowGroup = new TableRowGroup();
         rowGroup.Rows.Add(titleRow);
         rowGroup.Rows.Add(headerRow);

         string[][] results =
         {
            new[] {"1.", "Sebastian Vettel", "392"},
            new[] {"2.", "Jenson Button", "270"},
            new[] {"3.", "Mark Webber", "258"},
            new[] {"4.", "Fernando Alonso", "257"},
            new[] {"5.", "Lewis Hamilton", "227"}
         };

         var rows = results.Select(row =>
         {
            var tr = new TableRow();
            foreach (var cell in row)
               tr.Cells.Add(new TableCell(new Paragraph(new Run(cell))));
            return tr;
         }).ToList();

         rows.ForEach(r => rowGroup.Rows.Add(r));

         t1.RowGroups.Add(rowGroup);
         doc.Blocks.Add(t1);

         Reader.Document = doc;
      }
   }
}