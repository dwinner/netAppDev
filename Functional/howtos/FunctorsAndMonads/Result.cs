using System.Diagnostics;

namespace FunctorsAndMonads;

public sealed class Result<TVal, TErr>
{
   private readonly TErr _err;
   private readonly TVal _val;

   private Result(TVal val, TErr err, bool isSuccess)
   {
      _val = val;
      _err = err;
      IsSuccess = isSuccess;
   }

   public bool IsSuccess { get; }

   public TVal Val
   {
      get
      {
         Debug.Assert(IsSuccess, "Cannot fetch Value from a failed result.");
         return _val;
      }
   }

   public TErr Err
   {
      get
      {
         Debug.Assert(!IsSuccess, "Cannot fetch Error from a successful result.");
         return _err;
      }
   }

   public static Result<TVal, TErr> Ok(TVal val) => new(val, default!, true);

   public static Result<TVal, TErr> Fail(TErr err) => new(default!, err, false);

   public Result<TResult, TErr> Map<TResult>(Func<TVal, TResult> mapperFunc) =>
      IsSuccess
         ? Result<TResult, TErr>.Ok(mapperFunc(_val))
         : Result<TResult, TErr>.Fail(_err);

   public Result<TResult, TErr> Apply<TResult>(Result<Func<TVal, TResult>, TErr> resultFunc) =>
      resultFunc.IsSuccess && IsSuccess
         ? Result<TResult, TErr>.Ok(resultFunc.Val(Val))
         : Result<TResult, TErr>.Fail(resultFunc.IsSuccess
            ? _err
            : resultFunc.Err);

   public Result<TResult, TErr> Bind<TResult>(Func<TVal, Result<TResult, TErr>> monadFunc) =>
      IsSuccess
         ? monadFunc(_val)
         : Result<TResult, TErr>.Fail(_err);
}