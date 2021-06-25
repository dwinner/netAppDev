using System.Collections.Generic;

namespace TreeTownGame
{
   /// <summary>
   ///    Абстрактная сущность для представления узла дерева
   /// </summary>
   /// <typeparam name="T">Параметр типа узла</typeparam>
   public abstract class Node<T>
      where T : class
   {
      private readonly ISet<Node<T>> _matches = new HashSet<Node<T>>();

      /// <summary>
      ///    Конструктор узла
      /// </summary>
      /// <param name="aTown">Объект города</param>
      /// <param name="variants">Набор вариантов для проверки на совпадения</param>
      /// <param name="aParent">Родительский узел</param>
      protected Node(T aTown, ISet<T> variants, Node<T> aParent)
      {
         Town = aTown;
         Variants = variants;
         Parent = aParent;
      }

      /// <summary>
      ///    Конструктор узла
      /// </summary>
      /// <param name="aTown">Объект города</param>
      /// <param name="variants">Набор вариантов для проверки на совпадения</param>
      protected Node(T aTown, ISet<T> variants)
      {
         Town = aTown;
         Variants = variants;
         Parent = null;
         Depth = 0;
      }

      /// <summary>
      ///    Узел, для которого найдены совпадения
      /// </summary>
      public Node<T> Parent { get; private set; }

      /// <summary>
      ///    Исходный объект города для узла
      /// </summary>
      public T Town { get; private set; }

      /// <summary>
      ///    Варианты перебора городов
      /// </summary>
      protected ISet<T> Variants { get; private set; }

      /// <summary>
      ///    Глубина вложенности узла
      /// </summary>
      public uint Depth { get; protected set; }

      /// <summary>
      ///    Набор совпадений для узла
      /// </summary>
      public ISet<Node<T>> Matches
      {
         get { return _matches; }
      }

      /// <summary>
      ///    Template-метод построения цепочек объектов
      /// </summary>
      public abstract void BuidTownTree();

      #region Методы System.Object

      public override bool Equals(object obj)
      {
         var other = obj as Node<T>;
         if (other == null)
            return false;
         return Equals(_matches, other._matches) && Equals(Parent, other.Parent)
                && Equals(Town, other.Town) && Equals(Variants, other.Variants)
                && Depth == other.Depth;
      }

      public override int GetHashCode()
      {
         int hashCode = 0;
         unchecked
         {
            if (_matches != null)
               hashCode += 1000000007*_matches.GetHashCode();
            if (Parent != null)
               hashCode += 1000000009*Parent.GetHashCode();
            if (null != Town)
               hashCode += 1000000087*Town.GetHashCode();
            if (Variants != null)
               hashCode += 1000000093*Variants.GetHashCode();
            hashCode += 1000000097*Depth.GetHashCode();
         }
         return hashCode;
      }

      public override string ToString()
      {
         return string.Format("[Node: Count Matches={0}, Parent Town={1}, Town={2}, Depth={3}]",
            _matches.Count,
            Parent.Town,
            Town,
            Depth);
      }

      #endregion

      #region Перегруженные операторы

      public static bool operator ==(Node<T> lhs, Node<T> rhs)
      {
         if (ReferenceEquals(lhs, rhs))
            return true;
         if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
            return false;
         return lhs.Equals(rhs);
      }

      public static bool operator !=(Node<T> lhs, Node<T> rhs)
      {
         return !(lhs == rhs);
      }

      #endregion
   }
}