namespace IndexedSelection;

internal class TimeIndex(int index, DateTimeOffset time)
{
   public int Index { get; set; } = index;

   public DateTimeOffset Time { get; set; } = time;
}