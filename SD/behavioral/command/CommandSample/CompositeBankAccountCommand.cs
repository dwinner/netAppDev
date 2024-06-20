namespace CommandSample;

internal class CompositeBankAccountCommand : List<BankAccountCommand>, ICommand
{
   public void Call()
   {
      ForEach(cmd => cmd.Call());
   }

   public void Undo()
   {
      foreach (var cmd in ((IEnumerable<BankAccountCommand>)this).Reverse())
      {
         cmd.Undo();
      }
   }
}