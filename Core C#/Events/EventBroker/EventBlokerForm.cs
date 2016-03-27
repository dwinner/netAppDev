using System.Windows.Forms;

namespace EventBrokerDemo
{
   public partial class EventBlokerForm : Form
   {
      // Один брокер событий связывает все элементы управления
      readonly EventBroker _broker = new EventBroker();

      public EventBlokerForm()
      {
         InitializeComponent();

         myControl11.SetEventBroker(_broker);
         myControl21.SetEventBroker(_broker);
         myControl31.SetEventBroker(_broker);
      }
   }
}
