using System.Collections.ObjectModel;

namespace ExtendingCollectionSample;

public class AnimalCollection(Zoo? zoo) : Collection<Animal>
{
   protected override void InsertItem(int index, Animal item)
   {
      base.InsertItem(index, item);
      item.Zoo = zoo;
   }

   protected override void SetItem(int index, Animal item)
   {
      base.SetItem(index, item);
      item.Zoo = zoo;
   }

   protected override void RemoveItem(int index)
   {
      this[index].Zoo = null;
      base.RemoveItem(index);
   }

   protected override void ClearItems()
   {
      foreach (var animal in this)
      {
         animal.Zoo = null;
      }

      base.ClearItems();
   }
}