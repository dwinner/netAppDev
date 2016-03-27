using System.Windows.Forms;

namespace EventBrokerDemo
{
   public partial class MyControl3 : UserControl
   {
      EventBroker _broker;

      public MyControl3()
      {
         InitializeComponent();
      }

      public void SetEventBroker(EventBroker broker)
      {
         _broker = broker;
         _broker.Register("MyEvent", new MethodInvoker(OnMyEvent));
      }

      private void OnMyEvent()
      {
         labelResult.Text = "Event triggered!";
      }
   }
}
