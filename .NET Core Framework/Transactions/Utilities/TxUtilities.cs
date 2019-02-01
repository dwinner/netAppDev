using System;
using System.Transactions;

namespace Utilities
{
    public static class TxUtilities
    {
       public static bool AbortTransaction()
       {
          Console.Write("Abort the Transaction (y/n)?");
          var line = Console.ReadLine();
          return line != null && line.ToLower().Equals("y");
       }

       public static void DisplayTransactionInformation(string title, TransactionInformation transactionInfo)
       {
          //Contract.Requires<ArgumentNullException>(transactionInfo != null);

          Console.WriteLine(title);
          Console.WriteLine("Creation time: {0:T}", transactionInfo.CreationTime);
          Console.WriteLine("Status: {0}", transactionInfo.Status);
          Console.WriteLine("Local ID: {0}", transactionInfo.LocalIdentifier);
          Console.WriteLine("Distributed ID: {0}", transactionInfo.DistributedIdentifier);
          Console.WriteLine();
       }
    }
}
