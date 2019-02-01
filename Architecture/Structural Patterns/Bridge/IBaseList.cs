namespace Bridge
{
   /// <summary>
   ///    Base impl iface
   /// </summary>
   public interface IBaseList
   {
      int NumberOfItems { get; }
      string this[int index] { get; set; }
      bool SupportsOrdering { get; }
      void AddItem(string item);
      void RemoveItem(string item);
   }
}