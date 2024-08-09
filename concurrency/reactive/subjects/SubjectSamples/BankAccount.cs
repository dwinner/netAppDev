using System.Reactive.Subjects;

namespace SubjectSamples;

internal class BankAccount
{
   private readonly Subject<int> _inner = new();

   // BUG: don't do such thing
   public IObservable<int> MoneyTransactions => _inner;
}