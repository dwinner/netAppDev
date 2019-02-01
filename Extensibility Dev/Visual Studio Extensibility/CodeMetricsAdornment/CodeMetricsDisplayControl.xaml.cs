namespace CodeMetricsAdornment
{   
   public partial class CodeMetricsDisplayControl
   {
      private int _loc = 0;   // total lines of code
      private int _whitespace = 0;  // whitespace (empty) lines
      private int _comments = 0; // total lines that are comments

      public int LinesOfCode
      {
         get { return _loc; }
         set
         {
            _loc = value;
            Refresh();
         }
      }

      public int CommentLines
      {
         get { return _comments; }
         set
         {
            _comments = value;
            Refresh();
         }
      }

      public int WhitespaceLines
      {
         get { return _whitespace; }
         set
         {
            _whitespace = value;
            Refresh();
         }
      }

      private void Refresh()
      {
         CommentsTextBlock.Text = _comments.ToString();
         LocTextBlock.Text = _loc.ToString();
         WhitespaceTextBlock.Text = _whitespace.ToString();
      }

      public CodeMetricsDisplayControl()
      {
         InitializeComponent();
      }
   }
}