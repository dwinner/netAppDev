using System.Transactions;

namespace EF;

public class Transaction : IDisposable
{
   private readonly TransactionScope _transaction;
   public readonly string ConnectionString;

   public Transaction(string connectionString)
   {
      _transaction = new TransactionScope();
      ConnectionString = connectionString;
   }

   public void Dispose()
   {
      _transaction?.Dispose();
   }

   public void Commit()
   {
      _transaction.Complete();
   }
}