
// Copyright Christophe Bertrand.

using UniversalSerializerLib3;

namespace UniversalSerializer
{
   /// <summary>
   /// Public WPF error messages.
   /// </summary>
   public static class ErrorMessagesWpf
   {
      /// <summary>
      /// List of errors.
      /// </summary>
      public static ErrorMessages.ErrorDescriptor[] Errors = new ErrorMessages.ErrorDescriptor[]
      {

         new ErrorMessages.ErrorDescriptor() { ErrorNumber=0,
            Text= "Not an error (very strange !!)." },

         new ErrorMessages.ErrorDescriptor() { ErrorNumber=1,
            Text= "No public static field DependencyProperty {0} of type {1} found in type {2}." },

         new ErrorMessages.ErrorDescriptor() { ErrorNumber=2,
            Text= "The type '{0}' uses {1} as ValueSerializer, but it was not transcoded correctly. Please investigate or contact the author." },

      };

      /// <summary>
      /// Get a short WPF error description from its number.
      /// </summary>
      /// <param name="errorNumber"></param>
      /// <returns></returns>
      public static string GetText(int errorNumber)
      {
         return string.Format("Error WPF {0} : {1}", ErrorMessagesWpf.Errors[errorNumber].ErrorNumber.ToString(), ErrorMessagesWpf.Errors[errorNumber].Text);
      }
   }
}
