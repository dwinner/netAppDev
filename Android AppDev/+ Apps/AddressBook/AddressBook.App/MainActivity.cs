using Android.App;
using Android.Net;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using AppIds = AppDevUnited.AddressBook.App.Resource.Id;
using AppLayouts = AppDevUnited.AddressBook.App.Resource.Layout;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using FragmentV4 = Android.Support.V4.App.Fragment;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace AppDevUnited.AddressBook.App
{
   /// <summary>
   /// Управление фрагментами приложения и обмен данными между ними
   /// </summary>
   [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
   public class MainActivity :
      AppCompatActivity,
      ContactsFragment.IContactsFragmentListener,
      DetailFragment.IDetailFragmentListener,
      AddEditFragment.IAddEditFragmentListener
   {
      // Ключ для сохранения Uri контакта в переданном объекте Bundle
      public const string ContactUri = nameof(ContactUri);
      private const int PhoneFrameLayoutId = AppIds.fragmentContainer;
      private const int TabletRightPaneContainer = AppIds.rightPaneContainer;

      private ContactsFragment _contactsFragment;  // Вывод списка контактов

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
            _contactsFragment=new ContactsFragment();

            // Добавление фрагмента во FrameLayout
            FragmentTransaction transaction = SupportFragmentManager.BeginTransaction();
            transaction.Add(PhoneFrameLayoutId, _contactsFragment);
            transaction.Commit();   // Вывод ContactsFragment
         }
         else
         {
            _contactsFragment = (ContactsFragment) SupportFragmentManager.FindFragmentById(AppIds.contactsFragment);
         }
      }

      public void OnContactSelected(Uri contactsUri)  // Отображение DetailFragment для выбранного контакта
      {
         if (FindViewById<FrameLayout>(PhoneFrameLayoutId)!=null) // Телефон
         {
            DisplayContact(contactsUri, PhoneFrameLayoutId);
         }
         else // Планшет
         {
            // Извлечение с вершины стэка возврата
            SupportFragmentManager.PopBackStack();
            DisplayContact(contactsUri, TabletRightPaneContainer);
         }
      }

      private void DisplayContact(Uri contactsUri, int layoutId)
      {
         throw new System.NotImplementedException();
      }

      public void OnAddContact()
      {
         if (FindViewById<FrameLayout>(PhoneFrameLayoutId)!=null) // Телефон
         {
            DisplayAddEditFragment(PhoneFrameLayoutId, null);
         }
         else // Планшет
         {
            DisplayAddEditFragment(TabletRightPaneContainer,null);
         }
      }

      private void DisplayAddEditFragment(int layoutId, Uri contactsUri)
      {
         throw new System.NotImplementedException();
      }

      public void OnContactDeleted()
      {
         throw new System.NotImplementedException();
      }

      public void OnEditContact(Uri contactsUri)
      {
         throw new System.NotImplementedException();
      }

      public void OnAddEditCompleted(Uri uri)
      {
         throw new System.NotImplementedException();
      }
   }
}