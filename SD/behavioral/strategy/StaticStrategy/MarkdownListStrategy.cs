﻿using System.Text;

namespace StaticStrategy;

public class MarkdownListStrategy : IListStrategy
{
   public void Start(StringBuilder sb)
   {
      // markdown doesn't require a list preamble
   }

   public void End(StringBuilder sb)
   {
   }

   public void AddListItem(StringBuilder sb, string item)
   {
      sb.AppendLine($" * {item}");
   }
}