using System;
using System.Collections;
using System.Collections.Generic;
using GenericTypesWithConstraints;

public class LinkedList<T> : IEnumerable<T>
   where T : /* notnull,*/ ITitle
{
   public LinkedListNode<T>? First { get; private set; }
   public LinkedListNode<T>? Last { get; private set; }

   public IEnumerator<T> GetEnumerator()
   {
      var current = First;
      while (current is not null)
      {
         yield return current.Value;
         current = current.Next;
      }
   }

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   public LinkedListNode<T> AddLast(T node)
   {
      LinkedListNode<T> newNode = new(node);
      if (First is null || Last is null)
      {
         First = newNode;
         Last = First;
      }
      else
      {
         newNode.Prev = Last;
         var previous = Last;
         Last.Next = newNode;
         Last = newNode;
      }

      return newNode;
   }

   public void DisplayAllTitles()
   {
      foreach (var item in this)
      {
         Console.WriteLine(item.Title);
      }
   }
}