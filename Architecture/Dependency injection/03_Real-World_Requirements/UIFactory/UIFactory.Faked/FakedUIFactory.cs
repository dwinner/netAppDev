using UIFactory.Core;

namespace UIFactory.Faked
{
   public class FakedUiFactory : IUiFactory
   {
      public ButtonBase GetButton() => new FakedButton();

      public CheckBoxBase GetCheckBox() => new FakedCheckBox();
   }
}