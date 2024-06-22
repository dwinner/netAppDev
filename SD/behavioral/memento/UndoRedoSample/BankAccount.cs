namespace UndoRedoSample;

public class BankAccount // supports undo/redo
{
   private readonly List<Memento> _changes = [];
   private int _balance;
   private int _current;

   public BankAccount(int balance)
   {
      _balance = balance;
      _changes.Add(new Memento(balance));
   }

   public Memento Deposit(int amount)
   {
      _balance += amount;
      var memento = new Memento(_balance);
      _changes.Add(memento);
      ++_current;

      return memento;
   }

   public void Restore(Memento? memento)
   {
      if (memento == null)
      {
         return;
      }

      _balance = memento.Balance;
      _changes.Add(memento);
      _current = _changes.Count - 1;
   }

   public Memento? Undo()
   {
      if (_current <= 0)
      {
         return null;
      }

      var memento = _changes[--_current];
      _balance = memento.Balance;

      return memento;
   }

   public Memento? Redo()
   {
      if (_current + 1 < _changes.Count)
      {
         var m = _changes[++_current];
         _balance = m.Balance;
         return m;
      }

      return null;
   }

   public override string ToString() => $"{nameof(_balance)}: {_balance}";
}