namespace MVVMDemo
{
   public class WidgetViewModel : BaseViewModel
   {
      private readonly Widget _widget;

      public int Id { get { return _widget.Id; } }

      public string Name { get { return _widget.Name; } }

      public string WidgetType { get { return _widget.WidgetType.ToString(); } }

      public WidgetViewModel(Widget widget)
         : base("Widget")
      {
         _widget = widget;
      }
   }
}
