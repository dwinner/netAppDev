namespace BoolOverloading;

public struct SqlBoolean
{
   public static bool operator true(SqlBoolean x) => x._value == True._value;

   public static bool operator false(SqlBoolean x) => x._value == False._value;

   public static SqlBoolean operator !(SqlBoolean x) =>
      x._value == Null._value
         ? Null
         : x._value == False._value
            ? True
            : False;

   public static readonly SqlBoolean Null = new(0);

   public static readonly SqlBoolean False = new(1);

   public static readonly SqlBoolean True = new(2);

   private SqlBoolean(byte value) => _value = value;

   private readonly byte _value;
}