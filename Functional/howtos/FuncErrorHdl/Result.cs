namespace FuncErrorHdl;

public sealed class Result<TVal, TErr>
{
   private readonly TErr _error;
   private readonly TVal _value;

   private Result(TVal value, TErr error, bool isSuccess)
   {
      _value = value;
      _error = error;
      IsSuccess = isSuccess;
   }

   public bool IsSuccess { get; }

   public TVal Value
   {
      get
      {
         if (!IsSuccess)
         {
            throw new InvalidOperationException("Cannot fetch Value from a failed result.");
         }

         return _value;
      }
   }

   public TErr Error
   {
      get
      {
         if (IsSuccess)
         {
            throw new InvalidOperationException("Cannot fetch Error from a successful result.");
         }

         return _error;
      }
   }

   public static Result<TVal, TErr> Ok(TVal value) => new(value, default!, true);

   public static Result<TVal, TErr> Fail(TErr error) => new(default!, error, false);
}