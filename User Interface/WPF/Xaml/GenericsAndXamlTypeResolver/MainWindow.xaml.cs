namespace GenericsAndXamlTypeResolver
{
   /// Interaction logic for Window1.xaml
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }
   }

   // Test class
   public class MyGenericClass<T1, T2>
   {
      public T1 Prop1 { get; set; }

      public T2 Prop2 { get; set; }
   }
}