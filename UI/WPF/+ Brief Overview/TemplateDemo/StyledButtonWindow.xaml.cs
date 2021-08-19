namespace TemplateDemo
{
   public partial class StyledButtonWindow
   {
      public StyledButtonWindow()
      {
         InitializeComponent();
         SecondButton.Content = new Country {Name = "Austria", ImagePath = "images/Austria.bmp"};
      }
   }
}