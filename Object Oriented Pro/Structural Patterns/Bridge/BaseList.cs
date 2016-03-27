namespace Bridge
{
   /// <summary>
   /// Базовый класс иерархии абстракции
   /// </summary>
   public class BaseList
   {
      public IBaseListImpl BaseListImplementor { get; set; }

      public BaseList(IBaseListImpl baseListImpl)
      {
         BaseListImplementor = baseListImpl;
      }

      public BaseList() { }

      public void Add(string item)
      {
         BaseListImplementor.AddItem(item);
      }

      public void Add(string item, int position)
      {
         if (BaseListImplementor.SupportsOrdering)
            BaseListImplementor[position] = item;
      }

      public void Remove(string item)
      {
         BaseListImplementor.RemoveItem(item);
      }

      public virtual string Get(int index)
      {
         return BaseListImplementor[index];
      }

      public int Count()
      {
         return BaseListImplementor.NumberOfItems;
      }

      public string this[int i]
      {
         get { return Get(i); }
      }
   }

   public class NumberedList : BaseList
   {
      public NumberedList(IBaseListImpl baseListImpl)
         : base(baseListImpl)
      {
      }

      public NumberedList() { }

      public override string Get(int index)
      {
         return string.Format("{0}. {1}", (index + 1), base.Get(index));
      }
   }

   public class OrnamentedList : BaseList
   {
      private char _itemType;

      public char ItemType
      {
         get { return _itemType; }
         set
         {
            if (value > ' ')
               _itemType = value;
         }
      }

      public OrnamentedList(IBaseListImpl baseListImpl)
         : base(baseListImpl)
      {
      }

      public OrnamentedList() { }

      public override string Get(int index)
      {
         return string.Format("{0} {1}", _itemType, base.Get(index));
      }
   }
}
