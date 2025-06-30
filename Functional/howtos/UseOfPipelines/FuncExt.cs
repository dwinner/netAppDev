namespace UseOfPipelines;

public static class FuncExt
{
   public static T Pipe<T>(this T source, params Func<T, T>[] functions)
   {
      return functions.Aggregate(source, (current, func) => func(current));
   }
}