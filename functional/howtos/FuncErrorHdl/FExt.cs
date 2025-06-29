namespace FuncErrorHdl;

public static class FExt
{
   public static Result<TOut, TErr> Bind<TIn, TOut, TErr>(this Result<TIn, TErr> self,
      Func<TIn, Result<TOut, TErr>> bindFunc) =>
      self.IsSuccess
         ? bindFunc(self.Value)
         : Result<TOut, TErr>.Fail(self.Error);

   public static Result<TOut, TEOut> Bind<TIn, TOut, TEIn, TEOut>(this Result<TIn, TEIn> self,
      Func<TIn, Result<TOut, TEOut>> bindFunc,
      Func<TEIn, TEOut> errorMap) =>
      self.IsSuccess
         ? bindFunc(self.Value)
         : Result<TOut, TEOut>.Fail(errorMap(self.Error));

   public static async Task<Result<TOut, TErr>> BindAsync<TIn, TOut, TErr>(this Result<TIn, TErr> self,
      Func<TIn, Task<Result<TOut, TErr>>> bindFunc) =>
      self.IsSuccess
         ? await bindFunc(self.Value)
         : Result<TOut, TErr>.Fail(self.Error);

   public static bool IsFailure<TSuccess, TFailure>(this Result<TSuccess, TFailure> self) => !self.IsSuccess;

   public static Result<TVal, Exception> TryExecute<TVal>(Func<TVal> action)
   {
      try
      {
         return Result<TVal, Exception>.Ok(action());
      }
      catch (Exception ex)
      {
         return Result<TVal, Exception>.Fail(ex);
      }
   }

   public static Result<TVal, TErr> TryExecute2<TVal, TErr>(Func<TVal> action)
      where TErr : Exception
   {
      try
      {
         return Result<TVal, TErr>.Ok(action());
      }
      catch (TErr ex)
      {
         return Result<TVal, TErr>.Fail(ex);
      }
   }
}