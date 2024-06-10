using UIFactory.Core;
using UIFactory.Faked.Properties;

namespace UIFactory.Faked
{
   public sealed class FakedCheckBox : CheckBoxBase
   {
      public FakedCheckBox()
      {
         BackgroundImage = Resources.checkbox;
         Size = BackgroundImage.Size;
      }
   }
}