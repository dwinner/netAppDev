using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DiagnosticAdapter;

namespace UnitTests
{
   public class CommandListener
   {
      [DiagnosticName("Microsoft.EntityFrameworkCore.Database.Command.CommandExecuting")]
      public void OnCommandExecuting(DbCommand command, DbCommandMethod executeMethod, Guid commandId,
         Guid connectionId, bool async, DateTimeOffset startTime)
      {
      }

      [DiagnosticName("Microsoft.EntityFrameworkCore.Database.Command.CommandExecuted")]
      public void OnCommandExecuted(object result, bool async)
      {
      }
   }
}