using System;
using Microsoft.Extensions.Logging;

internal static class IdentityLogLevelExtensions
{
   public static LogLevel ToLogLevel(this Microsoft.Identity.Client.LogLevel logLevel)
      => logLevel switch
      {
         Microsoft.Identity.Client.LogLevel.Error => LogLevel.Error,
         Microsoft.Identity.Client.LogLevel.Warning => LogLevel.Warning,
         Microsoft.Identity.Client.LogLevel.Info => LogLevel.Information,
         Microsoft.Identity.Client.LogLevel.Verbose => LogLevel.Trace,
         _ => throw new InvalidOperationException("Update for a new log level")
      };
}