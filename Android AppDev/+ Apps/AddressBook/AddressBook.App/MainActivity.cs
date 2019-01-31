using Android.App;
using Android.Net;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using AppIds = AppDevUnited.AddressBook.App.Resource.Id;
using AppLayouts = AppDevUnited.AddressBook.App.Resource.Layout;
using FragmentV4 = Android.Support.V4.App.Fragment;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace AppDevUnited.AddressBook.App
{
   /// <summary>
   ///    Управление фрагментами приложения и обмен данными между ними
   /// </summary>
   [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
   public class MainActivity :
      AppCompatActivity,
      ContactsFragment.IContactsFragmentListener,
      DetailFragment.IDetailFragmentListener,
      AddEditFragment.IAddEditFragmentListener
   {
      // TOREFACTOR: Interface implementations had better to move in separated handlers
      // Ключ для сохранения Uri контакта в переданном объекте Bundle
      public const string ContactUri = nameof(ContactUri);
      private const int PhoneFrameLayoutId = AppIds.fragmentContainer;
      private const int TabletRightPaneContainer = AppIds.rightPaneContainer;

      private ContactsFragment _contactsFragment; // Вывод списка контактов

      /// <summary>
      ///    Обновление GUI после сохранения нового или существующего контакта
      /// </summary>
      /// <param name="contactUri">URI-контакта</param>
      public void OnAddEditCompleted(Uri contactUri)
      {
         SupportFragmentManager.PopBackStack(); // Удаление вершины стэка возврата

         if (FindViewById<FrameLayout>(PhoneFrameLayoutId) != null)
            SupportFragmentManager.PopBackStack(); // Удаление вершины стэка возврата для телефона
         else
            DisplayContact(contactUri, TabletRightPaneContainer);
      }

      /// <summary>
      ///    Отображение DetailFragment для выбранного контакта
      /// </summary>
      /// <param name="contactUri">URI-контакта</param>
      public void OnContactSelected(Uri contactUri)
      {
         if (FindViewById<FrameLayout>(PhoneFrameLayoutId) != null) // Телефон
         {
            DisplayContact(contactUri, PhoneFrameLayoutId);
         }
         else // Планшет
         {
            // Извлечение с вершины стэка возврата
            SupportFragmentManager.PopBackStack();
            DisplayContact(contactUri, TabletRightPaneContainer);
         }
      }

      /// <summary>
      ///    Отображение AddEditFragment для добавления нового контакта
      /// </summary>
      public void OnAddContact() =>
         DisplayAddEditFragment(
            FindViewById<FrameLayout>(PhoneFrameLayoutId) != null
               ? PhoneFrameLayoutId /* Телефон */
               : TabletRightPaneContainer, /* Планшет */
            null);

      /// <summary>
      ///    Возвращение к списку контактов для удаления текущего контакта
      /// </summary>
      public void OnContactDeleted()
      {
         // Удаление с вершины стэка
         SupportFragmentManager.PopBackStack();
         _contactsFragment.UpdateContactList();
      }

      /// <summary>
      ///    Отображение AddEditFragment для изменения существующего контакта
      /// </summary>
      /// <param name="contactUri">URI-контакта</param>
      public void OnEditContact(Uri contactUri) =>
         DisplayAddEditFragment(
            FindViewById<FrameLayout>(PhoneFrameLayoutId) != null
               ? PhoneFrameLayoutId /* Телефон */
               : TabletRightPaneContainer, /* Планшет */
            contactUri);

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(AppLayouts.activity_main);

         var toolbar = FindViewById<Toolbar>(AppIds.toolbar);
         SetSupportActionBar(toolbar);

         // Если макет содержит fragmentContainer, используется макет для телефона; отобразить ContactsFragment
         if (savedInstanceState != null
             && FindViewById<FrameLayout>(PhoneFrameLayoutId) != null)
         {
            // Создание ContactsFragment
            _contactsFragment = new ContactsFragment();

            // Добавление фрагмента во FrameLayout
            SupportFragmentManager.BeginTransaction().Add(PhoneFrameLayoutId, _contactsFragment).Commit();
         }
         else
         {
            _contactsFragment = (ContactsFragment) SupportFragmentManager.FindFragmentById(AppIds.contactsFragment);
         }
      }

      /// <summary>
      ///    Отображение информации о контакте
      /// </summary>
      /// <param name="contactUri">URI-контакта</param>
      /// <param name="viewId">Идентификатор представления</param>
      private void DisplayContact(IParcelable contactUri, int viewId)
      {
         // Передача URI контакта в аргументе DetailFragment
         var arguments = new Bundle();
         arguments.PutParcelable(ContactUri, contactUri);

         var detailFragment = new DetailFragment {Arguments = arguments};

         // Использование FragmentTransaction для отображения
         SupportFragmentManager.BeginTransaction().Replace(viewId, detailFragment).AddToBackStack(null).Commit();
      }

      /// <summary>
      ///    Отображение фрагмента для добавления или изменения контакта
      /// </summary>
      /// <param name="viewId">Идентификатор представления</param>
      /// <param name="contactUri">URI-контакта</param>
      private void DisplayAddEditFragment(int viewId, IParcelable contactUri)
      {
         var addEditFragment = new AddEditFragment();

         // При изменении передается аргумент contactUri
         if (contactUri != null)
         {
            var arguments = new Bundle();
            arguments.PutParcelable(ContactUri, contactUri);
            addEditFragment.Arguments = arguments;
         }

         // Использование FragmentTransaction для отображения AddEditFragment
         SupportFragmentManager.BeginTransaction().Replace(viewId, addEditFragment).AddToBackStack(null).Commit();
      }
   }
}