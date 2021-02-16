using System.Windows.Forms;
using ButtonBase = UIFactory.Core.ButtonBase;

namespace UIFactory.Real
{
   public class RealButton : ButtonBase
   {
      public RealButton()
      {
         var button = new Button {Text = "OK"};
         Size = button.Size;
         Controls.Add(button);
      }
   }
}