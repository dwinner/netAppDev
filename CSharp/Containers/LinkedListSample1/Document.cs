﻿namespace LinkedListSample
{
   public class Document
   {
      public string Title { get; private set; }

      public string Content { get; private set; }

      public byte Priority { get; private set; }

      public Document(string title, string content, byte priority)
      {
         Title = title;
         Content = content;
         Priority = priority;
      }
   }
}