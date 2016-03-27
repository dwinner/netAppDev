using System;
using System.Transactions;
using Castle.DynamicProxy;

namespace DataTransactionsCastle
{
   public class TransationWithRetries : IInterceptor
   {
      private readonly int _maxRetries;

      public TransationWithRetries(int maxRetries)
      {
         _maxRetries = maxRetries;
      }

      public void Intercept(IInvocation anInvocation)
      {
         var retries = _maxRetries;
         var succeeded = false;
         while (!succeeded)
         {
            using (var transactionScope = new TransactionScope())
            {
               try
               {
                  anInvocation.Proceed();
                  transactionScope.Complete();
                  succeeded = true;
               }
               catch (Exception)
               {
                  if (retries >= 0)
                  {
                     Console.WriteLine("Retrying {0}", anInvocation.Method.Name);
                     retries--;
                  }
                  else
                  {
                     throw;
                  }
               }
            }
         }
      }
   }
}