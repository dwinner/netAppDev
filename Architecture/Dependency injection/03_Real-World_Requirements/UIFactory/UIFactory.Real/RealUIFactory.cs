using UIFactory.Core;

namespace UIFactory.Real
{
   public class RealUiFactory : IUiFactory
   {
      public ButtonBase GetButton() => new RealButton();

      public CheckBoxBase GetCheckBox() => new RealCheckBox();
   }
}