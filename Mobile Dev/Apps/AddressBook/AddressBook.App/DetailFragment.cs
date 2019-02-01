using Android.Net;
using Android.Support.V4.App;

namespace AppDevUnited.AddressBook.App
{
   public class DetailFragment : Fragment
   {
      public interface IDetailFragmentListener
      {
         void OnContactDeleted();

         void OnEditContact(Uri contactUri);
      }
   }
}