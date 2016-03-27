using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTree
{
   class BinaryTree<T> : ICollection<T>
      where T : IComparable<T>
   {
      // Этот внутренний класс определен как закрытый,
      // потому что он нужен только дереву
      private class Node<TU>
      {
         public TU Value;
         // Эти поля открыты, чтобы мы могли использовать
         // модификатор "ref" для упрощения алгоритма         
         public Node<TU> LeftChild;
         public Node<TU> RightChild;

         public Node(TU val)
         {
            Value = val;
            LeftChild = RightChild = null;
         }
      }

      private Node<T> _root;

      public void Add(T item)
      {
         AddImpl(new Node<T>(item), ref _root);
      }

      // Удобный метод для одновременного добавления нескольких элементов
      public void Add(params T[] items)
      {
         foreach (T item in items)
         {
            Add(item);
         }
      }

      // Рекурсивно ищет место для вставки нового узла
      private void AddImpl(Node<T> newNode, ref Node<T> parentNode)
      {
         if (parentNode == null)
         {
            parentNode = newNode;
            Count++;
         }
         else
         {
            if (newNode.Value.CompareTo(parentNode.Value) < 0)
            {
               AddImpl(newNode, ref parentNode.LeftChild);
            }
            else
            {
               AddImpl(newNode, ref parentNode.RightChild);
            }
         }
      }

      public void Clear()
      {
         _root = null;
         Count = 0;
      }

      public bool Contains(T item)
      {
         return this.Any(val => val.CompareTo(item) == 0);
      }

      public void CopyTo(T[] array, int arrayIndex)
      {
         int index = arrayIndex;
         foreach (T val in InOrder)
         {
            array[index++] = val;
         }
      }

      public int Count { get; private set; }

      public bool IsReadOnly { get { return false; } }

      public bool Remove(T item)
      {
         bool removed = RemoveImpl(item, ref _root) != null;
         if (removed)
         {
            Count--;
         }
         return removed;
      }

      private static Node<T> RemoveImpl(T item, ref Node<T> node)
      {
         if (node == null)
         {
            return null;
         }
         if (node.Value.CompareTo(item) > 0)
         {
            node.LeftChild = RemoveImpl(item, ref node.LeftChild);
         }
         else if (node.Value.CompareTo(item) < 0)
         {
            node.RightChild = RemoveImpl(item, ref node.RightChild);
         }
         else
         {
            if (node.LeftChild == null)
            {
               node = node.RightChild;
            }
            else if (node.RightChild == null)
            {
               node = node.LeftChild;
            }
            else
            {
               Node<T> successor = FindSuccessor(node);
               node.Value = successor.Value;
               node.RightChild = RemoveImpl(successor.Value, ref node.RightChild);
            }
         }
         return node;
      }

      // Найти узел, следующий за указанным.
      // Цикл while возвращает самый левый узел на правой ветке
      private static Node<T> FindSuccessor(Node<T> node)
      {
         Node<T> currentNode = node.RightChild;
         while (currentNode.LeftChild != null)
         {
            currentNode = currentNode.LeftChild;
         }
         return currentNode;
      }

      public IEnumerator<T> GetEnumerator()
      {
         return InOrder.GetEnumerator();
      }

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      #region Далее определены итераторы, каждый из которых представлен открытым свойством и рекурсивной функцией, выполняющей всю работу

      public IEnumerable<T> PreOrder
      {
         get { return IteratePreOrder(_root); }
      }

      private static IEnumerable<T> IteratePreOrder(Node<T> parent)
      {
         if (parent == null)
            yield break;

         yield return parent.Value;

         foreach (T item in IteratePreOrder(parent.LeftChild))
         {
            yield return item;
         }
         foreach (T item in IteratePreOrder(parent.RightChild))
         {
            yield return item;
         }
      }

      public IEnumerable<T> PostOrder
      {
         get { return IteratePostOrder(_root); }
      }

      private static IEnumerable<T> IteratePostOrder(Node<T> parent)
      {
         if (parent == null)
            yield break;

         foreach (T item in IteratePostOrder(parent.LeftChild))
         {
            yield return item;
         }
         foreach (T item in IteratePostOrder(parent.RightChild))
         {
            yield return item;
         }

         yield return parent.Value;
      }

      public IEnumerable<T> InOrder
      {
         get { return IterateInOrder(_root); }
      }

      private static IEnumerable<T> IterateInOrder(Node<T> parent)
      {
         if (parent == null)
            yield break;

         foreach (T item in IterateInOrder(parent.LeftChild))
         {
            yield return item;
         }

         yield return parent.Value;

         foreach (T item in IterateInOrder(parent.RightChild))
         {
            yield return item;
         }
      }

      #endregion


   }

}
