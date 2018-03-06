namespace Bridge
{
   public sealed class NumberedList : BaseList
   {
      public NumberedList(IBaseList baseList)
         : base(baseList)
      {
      }

      public NumberedList()
      {
      }

      protected override string Get(int index) => $"{index + 1}. {base.Get(index)}";
   }
}