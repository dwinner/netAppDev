using System.Transactions;

namespace Transaction;

public class Transaction(string connectionString) : IDisposable
{
   private readonly TransactionScope _transaction = new();
   public readonly string ConnectionString = connectionString;

   public void Dispose() => _transaction.Dispose();

   public void Commit() => _transaction.Complete();
}