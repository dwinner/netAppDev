using System.Text;

namespace CSharp10Features.InterpolatedStringHandlers;

[InterpolatedStringHandler]
public ref struct LogInterpolatedStringHandler
{
   // Storage for the built-up string
   private readonly StringBuilder _builder;
   private readonly bool _enabled;

   public LogInterpolatedStringHandler(int literalLength, int formattedCount, Logger logger, LogLevel logLevel)
   {
      _enabled = logger.EnabledLevel >= logLevel;
      _builder = new StringBuilder(literalLength);
   }

   public void AppendLiteral(string s)
   {
      if (!_enabled)
      {
         return;
      }

      _builder.Append(s);
   }

   public void AppendFormatted<T>(T t)
   {
      if (!_enabled)
      {
         return;
      }

      _builder.Append(t?.ToString());
   }

   public void AppendFormatted<T>(T t, string format) where T : IFormattable
   {
      if (!_enabled)
      {
         return;
      }

      _builder.Append(t?.ToString(format, null));
   }

   internal string GetFormattedText() => _builder.ToString();
}
