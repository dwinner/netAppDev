using System;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using static AppDevUnited.AddressBook.App.Data.DatabaseDescription.Contact;
using FragmentV4 = Android.Support.V4.App.Fragment;
using LoaderV4 = Android.Support.V4.Content.Loader;
using LoaderManagerV4 = Android.Support.V4.App.LoaderManager;
using CursorLoaderV4 = Android.Support.V4.Content.CursorLoader;
using AppLayouts = AppDevUnited.AddressBook.App.Resource.Layout;
using AppIds = AppDevUnited.AddressBook.App.Resource.Id;
using Object = Java.Lang.Object;
using Uri = Android.Net.Uri;

namespace AppDevUnited.AddressBook.App
{ // TODO: Перенести реализацию интерфейса
   public class ContactsFragment : FragmentV4, LoaderManagerV4.ILoaderCallbacks
   {
      private const int ContactsLoader = 0; // Идентификатор Loader
      private ContactsAdapter _contactsAdapter; // Адаптер для recyclerView      
      private IContactsFragmentListener _listener; // Сообщает MainActivity о выборе контакта

      /// <summary>
      ///    Вызывается LoaderManager для создания Loader
      /// </summary>
      /// <param name="id">Id</param>
      /// <param name="args">Args</param>
      /// <returns>Loader</returns>
      public LoaderV4 OnCreateLoader(int id, Bundle args)
      {
         switch (id)
         {
            case ContactsLoader:
               return new CursorLoaderV4(Activity,
                  ContentUri,
                  null,
                  null,
                  null,
                  $"{ColumnName} COLLATE NOCASE ASC");

            default:
               return null;
         }
      }

      /// <summary>
      ///    Вызывается LoaderManager при завершении загрузки
      /// </summary>
      /// <param name="loader">Loader</param>
      /// <param name="data">Data</param>
      public void OnLoadFinished(LoaderV4 loader, Object data) => _contactsAdapter.SwapCursor((ICursor) data);

      /// <summary>
      ///    Вызывается LoaderManager при сбросе Loader
      /// </summary>
      /// <param name="loader"></param>
      public void OnLoaderReset(LoaderV4 loader) => _contactsAdapter.SwapCursor(null);

      public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
      {
         base.OnCreateView(inflater, container, savedInstanceState);
         HasOptionsMenu = true; // У фрагмента есть команды меню

         // Заполнение GUI и получение ссылки на RecyclerView
         var view = inflater.Inflate(AppLayouts.fragment_contacts, container, false);
         var recyclerView = view.FindViewById<RecyclerView>(AppIds.recyclerView);

         // recyclerView выводит элементы в вертикальном списке
         recyclerView.SetLayoutManager(new LinearLayoutManager(Activity.BaseContext));

         ContactsAdapter.IContactClickListener contactClickListener = new DefaultContactClickListenerImpl(this);

         // Создание адаптера recyclerView и слушателя щелчков на элементах
         _contactsAdapter = new ContactsAdapter(contactClickListener);
         recyclerView.SetAdapter(_contactsAdapter); // Назначение адаптера

         // Присоединение ItemDecorator для вывода разделителей
         recyclerView.AddItemDecoration(new ItemDivider(Context));

         // Улучшает быстродействие, если размер RecyclerView не изменяется
         recyclerView.HasFixedSize = true;

         // Получение FloatingActionButton и настройка обработчика
         var addButton = view.FindViewById<FloatingActionButton>(AppIds.addButton);
         addButton.Click += AddButton_Click;

         return view;
      }

      public override void OnAttach(Context context)
      {
         base.OnAttach(context);
         _listener = (IContactsFragmentListener) context;
      }

      public override void OnDetach()
      {
         base.OnDetach();
         _listener = null;
      }

      public override void OnActivityCreated(Bundle savedInstanceState)
      {
         base.OnActivityCreated(savedInstanceState);
         LoaderManager.InitLoader(ContactsLoader, null, this);
      }

      private void AddButton_Click(object sender, EventArgs e) => _listener.OnAddContact();

      /// <summary>
      ///    Обновление контактов
      /// </summary>
      public void UpdateContactList() => _contactsAdapter.NotifyDataSetChanged();

      /// <summary>
      ///    Методы обратного вызова, реализуемые MainActivity
      /// </summary>
      public interface IContactsFragmentListener
      {
         /// <summary>
         ///    Вызывается при выборе контакта
         /// </summary>
         /// <param name="contactUri">URI-контакта</param>
         void OnContactSelected(Uri contactUri);

         /// <summary>
         ///    Вызывается при нажатии кнопки добавления
         /// </summary>
         void OnAddContact();
      }

      private sealed class DefaultContactClickListenerImpl : ContactsAdapter.IContactClickListener
      {
         private readonly ContactsFragment _fragment;

         public DefaultContactClickListenerImpl(ContactsFragment fragment) => _fragment = fragment;

         public void OnClick(Uri contactUri) => _fragment._listener.OnContactSelected(contactUri);
      }
   }
}