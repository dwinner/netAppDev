using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;

namespace AppDevUnited.TwitterSearches.App
{
   public sealed class SearchesAdapter : RecyclerView.Adapter
   {
      private View.IOnClickListener _itemClickListener;
      private View.IOnLongClickListener _itemLongClickListener;
      private List<string> _tags;

      public SearchesAdapter(List<string> tags, View.IOnClickListener itemClickListener,
         View.IOnLongClickListener itemLongClickListener)
      {
         _tags = tags;
         _itemClickListener = itemClickListener;
         _itemLongClickListener = itemLongClickListener;
      }

      public override int ItemCount { get; }

      public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
      {
         throw new NotImplementedException();
      }

      public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) =>
         throw new NotImplementedException();

      public sealed class ViewHolder
      {
      }
   }
}