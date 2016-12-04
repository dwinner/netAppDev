namespace MVVMDemo
{
   public class Widget
   {
      public int Id { get; set; }

      public string Name { get; set; }

      public WidgetType WidgetType { get; set; }

      public Widget(int id, string name, WidgetType type)
      {
         Id = id;
         Name = name;
         WidgetType = type;
      }
   }
}
