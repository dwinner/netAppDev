namespace TemplateDemo
{
   public partial class StyledListBoxWindow
   {
      public StyledListBoxWindow()
      {
         InitializeComponent();
         DataContext = Countries.GetCountries();
      }
   }
}