public record LinkedListNode<T>(T Value)
   where T : notnull
//where T : unmanaged
{
   public LinkedListNode<T>? Next { get; internal set; }

   public LinkedListNode<T>? Prev { get; internal set; }

   public override string? ToString() => Value.ToString();
}