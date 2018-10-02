namespace TriggerDemo
{
   public partial class DataTriggerWindow
   {
      public DataTriggerWindow()
      {
         InitializeComponent();
         BookListBox.Items.Add(new Book {Title = "Professional C# 4.0", Publisher = "Wrox Press"});
         BookListBox.Items.Add(new Book {Title = "C# 2010 for Dummies", Publisher = "Dummies"});
         BookListBox.Items.Add(new Book {Title = "HTML and CSS: Design and Build Websites", Publisher = "Wiley"});
      }
   }
}