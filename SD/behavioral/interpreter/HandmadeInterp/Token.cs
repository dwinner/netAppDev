namespace HandmadeInterp;

public class Token(Token.Type type, string text)
{
   public enum Type
   {
      Integer,
      Plus,
      Minus,
      Lparen,
      Rparen
   }

   public Type MyType { get; } = type;

   public string Text { get; } = text;

   public override string ToString() => $"`{Text}`";
}