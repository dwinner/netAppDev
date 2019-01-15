using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using AppIds = AppDevUnited.TwitterSearches.App.Resource.Id;

namespace AppDevUnited.TwitterSearches.App
{
   /// <summary>
   ///    Адаптер для связывания данных с элементами RecyclerView
   /// </summary>
   public sealed class SearchesAdapter : RecyclerView.Adapter
   {
      // Слушатели главной активности, регистрируемые для каждого элемента списка
      private View.IOnClickListener _itemClickListener;
      private View.IOnLongClickListener _itemLongClickListener;

      // Список тэгов для хранения данных элементов RecyclerView
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

      /// <summary>
      ///    Вложенный класс, используемый для реализации пэттерна View-Holder в контексте
      ///    RecyclerView для логики повторного использования представлений
      /// </summary>
      private sealed class SearchViewHolder : RecyclerView.ViewHolder
      {
         private TextView _textView;

         public SearchViewHolder(View itemView, View.IOnClickListener clickListener,
            View.IOnLongClickListener longClickListener)
            : base(itemView)
         {
            _textView = itemView.FindViewById<TextView>(AppIds.textView);
            // TODO: С тем же успехом можно было создать Callback-методы вместо интерфейсов
            // Связывание слушателей с itemView
            itemView.SetOnClickListener(clickListener);
            itemView.SetOnLongClickListener(longClickListener);
         }
      }
   }
}