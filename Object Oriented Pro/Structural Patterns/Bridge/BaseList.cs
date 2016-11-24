namespace Bridge
{
   /// <summary>
   ///    Base class for abstraction
   /// </summary>
   public class BaseList
   {
      protected BaseList(IBaseList baseList)
      {
         BaseListImplementor = baseList;
      }

      public BaseList()
      {
      }

      public IBaseList BaseListImplementor { private get; set; }

      public string this[int i] => Get(i);

      public void Add(string item) => BaseListImplementor.AddItem(item);

      public void Add(string item, int position)
      {
         if (BaseListImplementor.SupportsOrdering)
         {
            BaseListImplementor[position] = item;
         }
      }

      public void Remove(string item) => BaseListImplementor.RemoveItem(item);

      protected virtual string Get(int index) => BaseListImplementor[index];

      public int Count() => BaseListImplementor.NumberOfItems;
   }
}