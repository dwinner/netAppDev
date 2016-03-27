using Wrox.Metro.Common;

namespace Wrox.Metro.DataModel
{
   public class MenuItem : BindableBase
   {
      private string text;
      public string Text
      {
         get { return text; }
         set
         {
            SetProperty(ref text, value);
            SetDirty();
         }
      }

      private void SetDirty()
      {
         if (MenuCard != null)
         {
            MenuCard.SetDirty();
         }
      }

      private double price;
      public double Price
      {
         get { return price; }
         set
         {
            SetProperty(ref price, value);
            SetDirty();
         }
      }

      public MenuCard MenuCard { get; set; }

   }
}
