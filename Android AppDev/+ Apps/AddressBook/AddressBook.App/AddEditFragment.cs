using Android.Net;
using Android.Support.V4.App;

namespace AppDevUnited.AddressBook.App
{
   public class AddEditFragment : Fragment
   {
      public interface IAddEditFragmentListener
      {
         void OnAddEditCompleted(Uri uri);
      }
   }
}