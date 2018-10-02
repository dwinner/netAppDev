using System;
using System.Windows.Forms;

namespace EventBrokerDemo
{
   public partial class MyControl1 : UserControl
   {
      EventBroker _broker;

      public MyControl1()
      {
         InitializeComponent();
      }

      public void SetEventBroker(EventBroker broker)
      {
         _broker = broker;
      }

      private void buttonTrigger_Click(object sender, EventArgs e)
      {
         if (_broker != null)
         {
            _broker.OnEvent("MyEvent");
         }
      }
   }
}
