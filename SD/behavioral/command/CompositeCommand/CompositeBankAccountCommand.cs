namespace CompositeCommand;

internal abstract class CompositeBankAccountCommand : List<BankAccountCommand>, ICommand
{
   public virtual void Call()
   {
      ForEach(cmd => cmd.Call());
   }

   public virtual void Undo()
   {
      foreach (var cmd in ((IEnumerable<BankAccountCommand>)this).Reverse())
      {
         cmd.Undo();
      }
   }

   public virtual bool Success { get; set; }
}