using LanguageExt;

namespace UseOf_Statics.Failures;

internal class UninitializedFailure(string what) : IAmFailure
{
   public string Reason { get; set; } = what;
   public static IAmFailure Create(string what) => new UninitializedFailure(what);
   public static Either<IAmFailure, T> Create<T>(string what) => new UninitializedFailure(what);
}