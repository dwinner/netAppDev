namespace ReferenceSemantics
{
   public class Container
   {
      private int[] _data;

      public Container(int[] data) => _data = data;

      public ref int GetItem(int index) => ref _data[index];

      public ref int[] GetData() => ref _data;

      public override string ToString() => string.Join(", ", _data);
   }
}