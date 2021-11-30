namespace CSharp10Features.InterpolatedStringHandlers;

public class Logger
{
   public LogLevel EnabledLevel { get; init; } = LogLevel.Error;

   public void LogMessage(LogLevel level, string msg)
   {
      if (EnabledLevel < level)
         return;

      Console.WriteLine(msg);
   }

   public void LogMessage(LogLevel level, [InterpolatedStringHandlerArgument("", "level")] LogInterpolatedStringHandler builder)
   {
      if (EnabledLevel < level)
         return;

      Console.WriteLine(builder.GetFormattedText());
   }
}
