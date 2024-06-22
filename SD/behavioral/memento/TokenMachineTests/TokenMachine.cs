namespace TokenMachineTests;

public class TokenMachine
{
   public List<Token> Tokens = [];

   public Memento AddToken(int value) => AddToken(new Token(value));

   public Memento AddToken(Token token)
   {
      Tokens.Add(token);

      // a rather roundabout way of cloning
      var snapshot = new Memento
      {
         Tokens = Tokens.Select(t => new Token(t.Value)).ToList()
      };

      return snapshot;
   }

   public void Revert(Memento memento)
   {
      Tokens = memento.Tokens.Select(mm => new Token(mm.Value)).ToList();
   }
}