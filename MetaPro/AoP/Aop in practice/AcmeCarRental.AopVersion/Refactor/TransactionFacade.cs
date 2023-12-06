using System;

namespace AcmeCarRental.AopVersion.Refactor
{
   public interface ITransactionFacade
   {
      void Wrapper(Action action);
   }

   public class TransactionFacade : ITransactionFacade
   {
      private readonly IExceptionHandler _exceptionHandler;
      private readonly ITransactionManager _transactionManager;

      public TransactionFacade(IExceptionHandler exceptionHandler, ITransactionManager transactionManager)
      {
         _exceptionHandler = exceptionHandler;
         _transactionManager = transactionManager;
      }
      
      public void Wrapper(Action action)
      {
         _exceptionHandler.Wrapper(() => { _transactionManager.Wrapper(action); });

         // or more concisely:
         //_exceptionHandler.Wrapper(() => _transactionManager.Wrapper(action));
      }
   }
}