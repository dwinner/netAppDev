using System;
using Android.Database;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using static AppDevUnited.AddressBook.App.Data.DatabaseDescription.Contact;
using Uri = Android.Net.Uri;

namespace AppDevUnited.AddressBook.App
{
   /// <summary>
   ///    Адаптер для RecyclerView
   /// </summary>
   public class ContactsAdapter : RecyclerView.Adapter
   {
      private readonly IContactClickListener _contactClickListener;
      private ICursor _cursor;

      public ContactsAdapter(IContactClickListener contactClickListener) =>
         _contactClickListener = contactClickListener;

      public override int ItemCount => _cursor?.Count ?? 0;

      public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
      {
         _cursor.MoveToPosition(position);
         var contactViewHolder = (ContactViewHolder) holder;
         contactViewHolder.RowId = _cursor.GetLong(_cursor.GetColumnIndex(Id));
         contactViewHolder.ItemTextView.Text = _cursor.GetString(_cursor.GetColumnIndex(ColumnName));
      }

      public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
      {
         // Заполнение SimpleListItem1
         var view = LayoutInflater.From(parent.Context).Inflate(Android.Resource.Layout.SimpleListItem1, parent, false);
         return new ContactViewHolder(this, view);
      }

      public void SwapCursor(ICursor cursor)
      {
         _cursor = cursor;
         NotifyDataSetChanged();
      }

      /// <summary>
      ///    Для обработки прикосновения к элементу в списке RecyclerView
      /// </summary>
      public interface IContactClickListener
      {
         void OnClick(Uri contactUri);
      }

      /// <summary>
      ///    Реализация View holder
      /// </summary>
      private sealed class ContactViewHolder : RecyclerView.ViewHolder
      {
         private readonly ContactsAdapter _adapter;

         public ContactViewHolder(ContactsAdapter adapter, View itemView) : base(itemView)
         {
            _adapter = adapter;
            ItemTextView = itemView.FindViewById<TextView>(Android.Resource.Id.Text1);
            itemView.Click += ItemView_Click;
         }

         public TextView ItemTextView { get; }

         /// <summary>
         ///    Идентификатор записи базы данных для контакта во ViewHolder
         /// </summary>
         public long RowId { private get; set; }

         private void ItemView_Click(object sender, EventArgs e) =>
            _adapter._contactClickListener.OnClick(BuildContactUri(RowId));
      }
   }
}