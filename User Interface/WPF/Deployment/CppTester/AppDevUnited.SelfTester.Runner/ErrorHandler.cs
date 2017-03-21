using System;
using System.IO;
using AppDevUnited.SelfTester.Model;

namespace AppDevUnited.SelfTester.Runner
{
   internal class ErrorHandler
   {
      private readonly Exception _unhandledEx;

      internal ErrorHandler(Exception unhandledEx)
      {
         _unhandledEx = unhandledEx;
      }

      internal Tuple<string, string> GetErrorMessage()
      {
         Tuple<string, string> errorMessagePair;

         var fnfEx = _unhandledEx as FileNotFoundException;
         if (fnfEx != null)
         {
            errorMessagePair = Tuple.Create("File is not found", fnfEx.FileName);
            return errorMessagePair;
         }

         var invalidSettingsEx = _unhandledEx as InvalidSettingsException;
         if (invalidSettingsEx != null)
         {
            errorMessagePair = Tuple.Create("Invalid Settings Detected", invalidSettingsEx.Message);
            return errorMessagePair;
         }

         var argumentEx = _unhandledEx as ArgumentException;
         if (argumentEx != null)
         {
            errorMessagePair = Tuple.Create("This column has already been selected", argumentEx.ParamName);
            return errorMessagePair;
         }

         errorMessagePair = Tuple.Create("Some Other Error Occured", _unhandledEx.Message);
         return errorMessagePair;
      }
   }
}