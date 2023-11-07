namespace AppContext.DbIdx;

public class BTreeNode<TKey, TPointer>
{
   private readonly int _degree;

   public BTreeNode(int degree)
   {
      _degree = degree;
      Children = new List<BTreeNode<TKey, TPointer>>(degree);
      Entries = new List<BTreeEntry<TKey, TPointer>>(degree);
   }

   public List<BTreeNode<TKey, TPointer>> Children { get; set; }

   public List<BTreeEntry<TKey, TPointer>> Entries { get; set; }

   public bool IsLeaf => Children.Count == 0;

   public bool HasReachedMaxEntries => Entries.Count == 2 * _degree - 1;

   public bool HasReachedMinEntries => Entries.Count == _degree - 1;
}