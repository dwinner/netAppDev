using System.Windows.Forms;
using UIFactory.Core;

namespace UIFactory.Real
{
   public class RealCheckBox : CheckBoxBase
   {
      public RealCheckBox()
      {
         var checkBox = new CheckBox();
         Controls.Add(new CheckBox());
         Size = checkBox.Size;
      }
   }
}