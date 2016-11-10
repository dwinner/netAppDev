namespace Bridge
{
   public sealed class OrnamentedList : BaseList
   {
      private char _itemType;

      public OrnamentedList(IBaseList baseList)
         : base(baseList)
      {
      }

      public OrnamentedList()
      {
      }

      public char ItemType
      {
         get { return _itemType; }
         set
         {
            if (value > ' ')
            {
               _itemType = value;
            }
         }
      }

      protected override string Get(int index) => $"{_itemType} {base.Get(index)}";
   }
}