using LanguageExt;

internal class Buffer
{
   private const int Count = 5;
   private readonly int[] _data = new int[Count];

   private readonly Func<Index, int[], bool> _idxGuard = (index, data) => index.Value >= 0 && index.Value < data.Length;

   public Option<int> this[Index index]
   {
      get => _idxGuard(index, _data) ? Option<int>.Some(_data[index]) : Option<int>.None;
      init
      {
         if (_idxGuard(index, _data))
         {
            var optValue = (int)value;
            _data[index] = optValue;
         }
      }
   }
}