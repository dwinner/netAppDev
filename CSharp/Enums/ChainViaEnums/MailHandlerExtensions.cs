using System;
using static ChainViaEnums.Address;
using static ChainViaEnums.GeneralDelivery;
using static ChainViaEnums.MailHandler;
using static ChainViaEnums.Readability;
using static ChainViaEnums.ReturnAddress;
using static ChainViaEnums.Scannability;

namespace ChainViaEnums
{
   public static class MailHandlerExtensions
   {
      public static bool HandleMail(this MailHandler @this, Mail mail)
      {
         switch (@this)
         {
            case MailHandler.GeneralDelivery:
               switch (mail.Delivery)
               {
                  case Yes:
                     Console.WriteLine($"Using general delivery for {mail}");
                     return true;
                  default:
                     return false;
               }

            case MachineScan:
               switch (mail.Scannability)
               {
                  case Unscannable:
                     return false;
                  default:
                     switch (mail.Address)
                     {
                        case Incorrect:
                           return false;
                        default:
                           Console.WriteLine("Delivering " + mail + " automatically");
                           return true;
                     }
               }

            case VisualInspection:
               switch (mail.Readability)
               {
                  case Illegible:
                     return false;
                  default:
                     switch (mail.Address)
                     {
                        case Incorrect:
                           return false;
                        default:
                           Console.WriteLine($"Delivering {mail} normally");
                           return true;
                     }
               }

            case ReturnToSender:
               switch (mail.ReturnAddress)
               {
                  case Missing:
                     return false;
                  default:
                     Console.WriteLine($"Returning {mail} to sender");
                     return true;
               }

            default:
               throw new ArgumentOutOfRangeException(nameof(@this), @this, null);
         }
      }
   }
}